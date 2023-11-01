// Author: Mithul Koshy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen;
    [SerializeField] UIPoolBar hpBar;

    GameObject currentHoverOverObject;
    [HideInInspector]
    public InteractableObject hoveringObject;
    CharacterDamage hoveringOverCharacter;

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
            if(currentHoverOverObject!=hit.transform.gameObject)
            {
                currentHoverOverObject = hit.transform.gameObject;
                UpdateInteractableObject(hit);

            }
        }
    }

    private void UpdateInteractableObject(RaycastHit hit)
    {
        InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            hoveringObject = interactableObject;
            hoveringOverCharacter = interactableObject.GetComponent<CharacterDamage>();
            textOnScreen.text = hoveringObject.objectName;
        }
        else
        {
            hoveringOverCharacter = null;
            hoveringObject = null;
            textOnScreen.text = "";

        }
        UpdateHPBar();

    }
    private void UpdateHPBar()
    {
        if(hoveringOverCharacter != null)
        {
            hpBar.Show(hoveringOverCharacter.lifepool);
            Debug.Log(hoveringOverCharacter.lifepool);
        }
        else
        {
            hpBar.Clear();
        }
    }
}
