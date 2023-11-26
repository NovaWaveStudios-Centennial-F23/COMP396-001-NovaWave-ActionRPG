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

    // Layer mask for the "UI" layer
    public LayerMask uiLayerMask;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (isLocalPlayer)
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

                characterMovement.CmdSetDestination(mouseInput.mouseInputPosition);
            }
        }
        
    }
}
