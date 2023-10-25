using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelectButton : GroupButton
{

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
    }

}
