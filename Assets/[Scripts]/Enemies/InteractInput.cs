using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen;
    InteractableObject hoveringObject;
    void Update()
    {
        CheckInteractObject();
        if (Input.GetMouseButtonDown(0))
        {
            if (hoveringObject != null)
            {
                hoveringObject.Interact();
            }



        }
    }

    private void CheckInteractObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                hoveringObject = interactableObject;
                textOnScreen.text = hoveringObject.objectName;
            }
            else
            {
                hoveringObject = null;
                textOnScreen.text = "";
            }
        }
    }
}
