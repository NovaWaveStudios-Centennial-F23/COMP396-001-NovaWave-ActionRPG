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

    private void Start()
    {
        icon.color = UIConstants.deselectedSkillColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    { 

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
        icon.sprite = ActiveSkillUIData.Instance.GetSprite(skillName);
    }

}
