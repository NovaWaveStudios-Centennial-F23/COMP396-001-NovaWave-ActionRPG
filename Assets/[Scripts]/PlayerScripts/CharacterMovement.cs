// Author: Mithul Koshy
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : NetworkBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
   private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    [Command]
    public void CmdSetDestination(Vector3 destinationPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(destinationPosition);
    }

    internal void Stop()
    {
        agent.isStopped = true;
    }
}
