// Author: Mithul Koshy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    float damageAmount = 10f;
    //Stats stats;
    [SerializeField] float attackRange = 1f;
    Animator animator;
    CharacterMovement characterMovement;
    UIPoolBar uiPoolBar;
    InteractableObject target;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        //Stats stats = GetComponent<Stats>();
        CharacterDamage characterDamage = GetComponent<CharacterDamage>();
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
            CharacterDamage targetCharactertoAttack = target.GetComponent<CharacterDamage>();
            targetCharactertoAttack.TakeDamage(damageAmount);

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
