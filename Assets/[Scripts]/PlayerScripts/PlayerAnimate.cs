using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimate : NetworkBehaviour
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
        if (isServer)
        {
            float motion = agent.velocity.magnitude;
            animator.SetFloat("Motion", motion);
        }
        
    }
}
