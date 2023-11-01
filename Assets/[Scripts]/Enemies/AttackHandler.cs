// Author: Mithul Koshy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackHandler : MonoBehaviour
{
    float damageAmount = 10f;
    //Stats stats;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float defaultTimeToAttack = 2f;
    float attackTimer;

    Animator animator;
    CharacterMovement characterMovement;
    CharacterDamage target;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        //Stats stats = GetComponent<Stats>();
        CharacterDamage characterDamage = GetComponent<CharacterDamage>();
    }
    internal void Attack(CharacterDamage target)
    {
        this.target = target;
        ProcessAttack();
    }

    private void Update()
    {
        AttackTimerTick();
        if(target != null)
        {
            ProcessAttack();
        }
    }

    private void AttackTimerTick()
    {
        if (attackTimer>0f)
        {
            attackTimer -= Time.deltaTime;

        }
    }

    private void ProcessAttack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            if(attackTimer>0f)
            {
                return;
            }
            attackTimer = defaultTimeToAttack;
            characterMovement.Stop();
            animator.SetTrigger("Attack");
            target.TakeDamage(damageAmount);

            //Code to be modified
            //Stats targetCharactertoAttack = target.GetComponent<Stats.Stat.Health>;
            //targetCharactertoAttack.TakeDamage(character.TakeStats(Statistic.Damage).value);
            target = null;

        }
        else
        {
            characterMovement.SetDestination(target.transform.position);
        }
    }
}
