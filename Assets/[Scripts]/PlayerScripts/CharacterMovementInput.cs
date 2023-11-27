/**Created by Mithul Koshy
 * Used to check character movement and mouse clicks in Game
 */
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovementInput : NetworkBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovement characterMovement;
    Health health; // Reference to the Health script
    private bool isCastingSpell = false; // Flag to track if a spell is being cast

    // Layer mask for the "UI" layer
    public LayerMask uiLayerMask;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        health = GetComponent<Health>(); // Get the Health component
    }

    private void Update()
    {
        if (health.currentState == Health.CharacterState.Alive && !isCastingSpell && isLocalPlayer)
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

                if (mouseInput == null)
                {
                    mouseInput = Camera.main.GetComponent<MouseInput>();
                }

                characterMovement.CmdSetDestination(mouseInput.mouseInputPosition);            }
        }

        // Handle spell casting logic for the player with the "Player" tag
        if (gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
            {
                // Set isCastingSpell to true to prevent movement
                isCastingSpell = true;

            }
            else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.R))
            {
                // After the spell is cast, set isCastingSpell back to false to allow movement
                isCastingSpell = false;

            }
        }
        
    }
}
