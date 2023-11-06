using System;
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
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            if (attackTimer > 0f)
            {
                return;
            }
            attackTimer = defaultTimeToAttack;
            characterMovement.Stop();
            animator.SetTrigger("Attack");

            // Determine damage amount based on the target's tag
            float damageAmount = target.gameObject.CompareTag("Player") ? playerDamageAmount : enemyDamageAmount;

            target.TakeDamage(damageAmount);
            target = null;
        }
        else
        {
            characterMovement.SetDestination(target.transform.position);
        }
    }
}
