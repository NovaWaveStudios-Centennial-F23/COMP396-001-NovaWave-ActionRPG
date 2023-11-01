// Author: Mithul Koshy
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
   private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void SetDestination(Vector3 destinationPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(destinationPosition);
    }

    internal void Stop()
    {
        agent.isStopped = true;
    }
}
