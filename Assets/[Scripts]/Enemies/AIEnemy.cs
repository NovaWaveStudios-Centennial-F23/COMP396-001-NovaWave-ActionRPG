using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    AttackHandler attackHandler;
    NavMeshAgent navMeshAgent;

    [SerializeField] float detectionRadius = 3f;
    [SerializeField] float movementSpeed = 3.5f;
    Health target;
    float timer = 4f;

    private void Awake()
    {
        Debug.Log("AIENEMY AWAKE CALLED");
        attackHandler = GetComponent<AttackHandler>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    private void Update()
    {
        Debug.Log("UPDATE CALLED");
        if (target == null)
        {
            FindClosestTarget();
        }

        if (target != null)
        {
            ChaseTarget();
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                timer = 4f;
                attackHandler.Attack(target);
                Debug.Log("ATTACKING TARGET");
            }
        }
    }

    private void FindClosestTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float closestDistance = detectionRadius;
        Health closestTarget = null;

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Health health = hitCollider.GetComponent<Health>();
                if (health != null)
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = health;
                    }
                }
            }
        }

        target = closestTarget;
        if (target != null)
        {
            Debug.Log("CLOSEST PLAYER TARGETED");
        }
    }

    private void ChaseTarget()
    {
        Debug.Log("CHASING TARGET");
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
