using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    // Base damage amounts for different targets
    [SerializeField] private float playerDamageAmount = 30f;
    [SerializeField] private float enemyDamageAmount = 50f;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float defaultTimeToAttack = 2f;
    private float attackTimer;

    private Animator animator;
    private CharacterMovement characterMovement;
    private Health target;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    internal void Attack(Health target)
    {
        this.target = target;
        ProcessAttack();
    }

    private void Update()
    {
        AttackTimerTick();
        if (target != null)
        {
            ProcessAttack();
        }
    }

    private void AttackTimerTick()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void ProcessAttack()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (attackTimer > 0f)
        {
            return;
        }

        // Determine damage amount based on the attacker's tag
        float damageAmount = 0f;

        if (gameObject.CompareTag("Player") && target.gameObject.CompareTag("Enemy"))
        {
            // Player attacks enemy
            damageAmount = playerDamageAmount;
        }
        else if (gameObject.CompareTag("Enemy") && target.gameObject.CompareTag("Player"))
        {
            // Enemy attacks player
            damageAmount = enemyDamageAmount;
        }

        if (damageAmount > 0f && distance < attackRange)
        {
            attackTimer = defaultTimeToAttack;
            characterMovement.Stop();
            animator.SetTrigger("Attack");
            target.TakeDamage(damageAmount);
        }
        else
        {
            // Limit the maximum distance to move towards the target
            float maxMoveDistance = 4f;
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            Vector3 targetPosition = transform.position + directionToTarget * Mathf.Min(distance, maxMoveDistance);
            characterMovement.SetDestination(targetPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
