using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
   private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    public void SetDestination(Vector3 destinationPosition)
    {
        agent.SetDestination(destinationPosition);
    }
}
