using System.Collections.Generic;
using UnityEngine;

public class AttackControler : MonoBehaviour
{
    [Header("WarriorHP")]
    [SerializeField] private WarriorHP warriorHP;
    [Header("State")]
    public bool isAttacking;
    [Header("Componenets")]
    [SerializeField] private MovementControler movementController;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPos;
    [SerializeField] private AudioSource axeAudio;

    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    
    [Header("Dictionary")]
    private Dictionary<KeyCode, string> attackAnimations =new Dictionary<KeyCode, string>()
    {
        {KeyCode.J, "Attack1"},
        {KeyCode.K, "Attack2"},
        {KeyCode.L, "Attack3"}
    };

    private float timeDelay = 0.5f;
    private float coolDownTimer = 0f;

    void Start()
    {
        warriorHP = GetComponentInParent<WarriorHP>();
        movementController = GetComponentInParent<MovementControler>();
        
    }
    void Update()
    {
        if (warriorHP.isAlive())
        {
            coolDownTimer -= Time.deltaTime;
            bool isRunning = movementController.currentState == movingState.isRunning;
            foreach (var attack in attackAnimations)
            {
                if (Input.GetKeyDown(attack.Key) && CanAttack())
                {
                    isAttacking = true;
                    HandleAttack(attack.Value, isRunning);
                    break;
                }
            }
        }
    }
    private bool CanAttack()
    {
        return coolDownTimer <= 0;
    }
    private void ResetAttack()
    {
        coolDownTimer = timeDelay;
    }

    private void HandleAttack(string animationTrigger, bool isRunning)
    {
        ResetAttack();
        if (animationTrigger == "Attack1" && isRunning)
        {
            animator.SetTrigger("Run Attack");
        }
        else
        {
            animator.SetTrigger(animationTrigger);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void AttackStart()
    {
        if (axeAudio != null)
        {
            axeAudio.Play();
        }
    }

    private void AttackEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        foreach (var collider in colliders)
        {
            EnemyHP enemy = collider.GetComponent<EnemyHP>();
            enemy.TakeDamage(damage);
        }
    }
    private void EndAttacking()
    {
        isAttacking = false;
    }

    public void increaseDamage(int damageAmount)
    {
        damage += damageAmount;
    }

    public int GenerateAmount(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
}
