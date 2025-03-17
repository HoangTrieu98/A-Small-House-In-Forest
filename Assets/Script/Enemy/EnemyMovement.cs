using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    isStanding, isRunning
}


public class EnemyMovement : MonoBehaviour
{
    [Header("Warrior")]
    [SerializeField] private WarriorHP warriorHp;
    [Header("State")]
    public SpawnSide spawnSide;
    public EnemyState currentState;
    public bool isInRange;
    private EnemyHealthState healthState;
    [SerializeField] private bool isReadyToFollow;
    [SerializeField] private bool isFacingRight;
    [Header("Components")]
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigid;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float withinRange;
    [SerializeField] private float idleTime;
    private float currentIdleTime;
    private Vector2 direction;
    private Coroutine moveCoroutine;
    private float distanceToTarget;


    // Start is called before the first frame update
    void Start()
    {
        healthState = GetComponentInChildren<EnemyHealthState>();
        currentState = EnemyState.isStanding;
        isFacingRight = true;
        isInRange = false;
        isReadyToFollow = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            warriorHp = player.GetComponent<WarriorHP>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (warriorHp != null && warriorHp.isAlive())
        {
            if (target != null)
            {
                UpdateDistance();
                UpdateDirection();
                if (isReadyToFollow)
                {
                    if (healthState.isHurt == false && healthState.isDead == false)
                    {
                        Moving();
                    }
                }
                else
                {
                    ResetReady();
                }
            }
        }
        else
        {
            target = null;
            isInRange = false;
            rigid.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
        }
    }

    private void ResetReady()
    {
        currentIdleTime += Time.deltaTime;
        if (currentIdleTime >= idleTime)
        {
            isReadyToFollow = true;
            currentIdleTime = 0f;
        }
    }

    private void UpdateDistance()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.position);
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        direction = (target.position-transform.position).normalized;
         if (isFacingRight)
        {
            if (direction.x < 0)
            {
                isFacingRight = false;
                localScale.x = -1;
            }
        }
        else
        {
            if (direction.x > 0)
            {
                isFacingRight = true;
                localScale.x = 1;
            }
        }
        transform.localScale = localScale;
    }

    private void Moving()
    {
        Vector2 directionX = new Vector2(direction.x, 0);

        if (distanceToTarget > withinRange && isReadyToFollow)
        {
            isInRange = false;
            currentState = EnemyState.isRunning;
            rigid.velocity = directionX * moveSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            isInRange = true;
            isReadyToFollow = false;
            currentState = EnemyState.isStanding;
            rigid.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
        }
    }
}
