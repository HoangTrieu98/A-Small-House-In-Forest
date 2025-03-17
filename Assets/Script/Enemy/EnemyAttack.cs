using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] private EnemyHP enemyHp;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask warriorLayer;
    [SerializeField] private Transform attackPos;
    [SerializeField] private AudioSource attackSound;

    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeDelay;
    private float coolDownTimer = 0f;

    void Start()
    {
        enemyHp = GetComponentInParent<EnemyHP>();
        enemyMovement = GetComponentInParent<EnemyMovement>();  
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Time.deltaTime;
        bool canAttack = enemyMovement.isInRange;
        if (canAttack && coolDownTimer <= 0 && enemyHp.isAlive())
        {
            coolDownTimer = timeDelay;
            Attacking();
        }
    }

    private void Attacking()
    {
        animator.SetTrigger("Attack");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void AttackEvent()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos.position, attackRange, warriorLayer);
        foreach(var hit in hits)
        {
            WarriorHP warriorHp = hit.GetComponent<WarriorHP>();
            warriorHp.TakeDamage(damage);
        }
    }

    private void AttackStart()
    {
        if (attackSound != null)
        {
            attackSound.Play();
        }
        else
        {
            return;
        }
    }
}
