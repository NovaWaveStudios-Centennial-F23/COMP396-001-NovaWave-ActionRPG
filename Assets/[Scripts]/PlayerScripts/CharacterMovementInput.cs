// Author: Mithul Koshy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovement characterMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            characterMovement.SetDestination(mouseInput.mouseInputPosition);

        }
    }
}
