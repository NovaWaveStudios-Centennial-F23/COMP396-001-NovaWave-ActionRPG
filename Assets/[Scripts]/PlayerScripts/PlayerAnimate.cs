using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimate : MonoBehaviour
{
    Animator animator; // Change the variable type to Animator

    NavMeshAgent agent;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // Get the Animator component
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float motion = agent.velocity.magnitude;
        animator.SetFloat("Motion", motion);
    }
}
