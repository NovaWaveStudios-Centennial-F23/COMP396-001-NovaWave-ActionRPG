using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : GroupButton
{
    [Header("Character Select Button")]
    [SerializeField]
    TextMeshProUGUI txtCharacterName;

    [SerializeField]
    TextMeshProUGUI txtCharacterLevel;

    CharacterSelector characterSelector;
    public string characterName;
    public int characterLevel;
    public int saveNumber;
    Sprite characterIcon;

    private void Awake()
    {
        characterSelector = FindFirstObjectByType<CharacterSelector>();
        UpdateAppearance();
    }

    public void PopulateData(CharacterSaveData data)
    {
        characterName = data.Name;
        characterLevel = data.Level;
        saveNumber = data.SaveNumber;
    }

    protected override void UpdateAppearance()
    {
        if (isActive)
        {
            background.color = UIConstants.selectedCharacterBorderColour;
            icon.color = UIConstants.selectedSkillColor;
        }
        else
        {
            background.color = UIConstants.unselectedCharacterBorderColour;
            icon.color = UIConstants.deselectedSkillColor;
        }

        //icon.sprite = characterIcon;
        txtCharacterLevel.text = $"Level: {characterLevel}";
        txtCharacterName.text = characterName;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(characterSelector == null)
        {
            characterSelector = FindFirstObjectByType<CharacterSelector>();
        }


        if(characterSelector != null)
        {
            base.OnPointerClick(eventData);
            characterSelector.SetSelection(gameObject);
        }
        else
        {
            Debug.LogError($"Cannot find {nameof(CharacterSelector)} for {this}");
        }
    }

    public void UpdateSprite(Sprite sprite)
    {
        characterIcon = sprite;
        UpdateAppearance();
    }

    public void UpdateName(string name)
    {
        characterName = name;
        UpdateAppearance();
    }

    public void UpdateLevel(int level)
    {
        characterLevel = level;
        UpdateAppearance();
    }



}
