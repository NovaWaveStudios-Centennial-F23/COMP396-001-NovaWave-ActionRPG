/**Created by Han Bi
 * UI object to allow player to select what spell to cast on which key
 * this component will pass the link between skill and key to another component to handle
 */
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    //used to track what instance of this is displaying 
    public static SkillSlot displayingSkillSelection;

    [SerializeField]
    [Tooltip("The skill will activate using this key")]
    KeyCode activationKey;

    [SerializeField]
    [Tooltip("The text that will display the key to player")]
    TextMeshProUGUI txtKeyText;

    [SerializeField]
    GameObject tooltip;

    //references the icon image component
    [SerializeField]
    Image iconImage;

    [SerializeField]
    Transform skillSelectionContainer;

    [SerializeField]
    GameObject skillSelectionButton;

    //this is a reference of the skillname
    private string selectedSkill;


    public string GetSelectedSkill()
    {
        return selectedSkill;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //show an option of all skills the player has unlocked
        ShowToolTip();

    }

    private void Start()
    {
        //ensure tooltip is hidden
        HideToolTip();
        txtKeyText.text = activationKey.ToString();
        iconImage = GetComponent<Image>();
                
    }

    public void SetSkill(string skill)
    {

        HideToolTip();
        //guards?
        selectedSkill = skill;

        //change the icon image
        if(iconImage == null)
        {
            iconImage = GetComponent<Image>();
        }
        iconImage.sprite = ActiveSkillUIData.Instance.GetSprite(skill);

        //let the input manager know
        InputController.Instance.RegisterSpell(activationKey, skill);

    }

    public void HideToolTip()
    {
        if(tooltip != null && tooltip.activeInHierarchy)
        {
            tooltip.SetActive(false);
        }
        
    }

    private void ShowToolTip()
    {
        if (!tooltip.activeInHierarchy)
        {
            if(displayingSkillSelection != null)
            {
                displayingSkillSelection.HideToolTip();
            }
            tooltip.SetActive(true);
            displayingSkillSelection = this;
        }
    }

    public void Reset()
    {
        InputController.Instance.UnRegisterSpell(activationKey);
        selectedSkill = "";
        iconImage.sprite = null;
    }



}
