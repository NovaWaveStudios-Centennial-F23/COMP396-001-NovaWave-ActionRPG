// Author: Mithul Koshy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    Stats stats;
    [SerializeField] float attackRange = 1f;
    Animator animator;
    CharacterMovement characterMovement;

    InteractableObject target;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        //Stats stats = GetComponent<Stats>();
    }
    internal void Attack(InteractableObject target)
    {
        this.target = target;
        ProcessAttack();
    }

    private void Update()
    {
        if(target != null)
        {
            ProcessAttack();
        }
    }

    private void ProcessAttack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            characterMovement.Stop();
            animator.SetTrigger("Attack");
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
