using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSelectionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string skillName;

    [SerializeField]
    Image icon;

    public event Action<string> OnSkillSelected = delegate { };
    private void Start()
    {
        icon.color = UIConstants.deselectedSkillColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSkillSelected(skillName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.color = UIConstants.selectedSkillColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.color = UIConstants.deselectedSkillColor;
    }

    public void SetSkill(string skill)
    {
        skillName = skill;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        //currently this will throw an error if it is active when scene is generated since instance has not been set yet when attempting to query
        try
        {
            icon.sprite = ActiveSkillUIData.Instance.GetSprite(skillName);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

}
