// Author: Mithul Koshy
using System;
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
    [HideInInspector]
    public Health hoveringOverCharacter;

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
                try
                {
                    UpdateInteractableObject(hit);

                }catch(Exception e)
                {
                    //Debug.LogWarning(e);
                }
                
            }
        }
    }

    private void UpdateInteractableObject(RaycastHit hit)
    {
        InteractableObject interactableObject; 
        try
        {
            interactableObject = hit.transform.GetComponent<InteractableObject>();
        }
        catch
        {
            interactableObject = null;
        }
        

        if (interactableObject != null)
        {
            hoveringObject = interactableObject;
            hoveringOverCharacter = interactableObject.GetComponent<Health>();
            if(textOnScreen == null)
            {
                textOnScreen = InGameUIManager.Instance.txtEnemyName;
            }
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
            if(hpBar == null)
            {
                hpBar = InGameUIManager.Instance.enemyHealthBarDisplay;
            }
            hpBar.Show(hoveringOverCharacter.lifepool);
            //Debug.Log(hoveringOverCharacter.lifepool);
        }
        else
        {
            if (hpBar == null)
            {
                hpBar = InGameUIManager.Instance.enemyHealthBarDisplay;
            }
            hpBar.Clear();
        }
    }
}
