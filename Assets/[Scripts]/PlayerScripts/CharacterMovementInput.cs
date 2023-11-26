/**Created by Mithul Koshy
 * Used to check character movement and mouse clicks in Game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovementInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovement characterMovement;
    Health health; // Reference to the Health script

    // Layer mask for the "UI" layer
    public LayerMask uiLayerMask;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        health = GetComponent<Health>(); // Get the Health component
    }

    private void Update()
    {
        if (health.currentState == Health.CharacterState.Alive) // Check character's state
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                // Perform a raycast to check the layer of the clicked object
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayerMask))
                {
                    return;
                }

                characterMovement.SetDestination(mouseInput.mouseInputPosition);
            }
        }
    }
}
