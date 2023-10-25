using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelectionHandler : GroupButtonHandler
{
    public event Action<ClassDescriptionSO> OnCharacterSelected = delegate { };
    protected override void HandleButtonStateChange(GroupButton button)
    {
        base.HandleButtonStateChange(button);

        if(button.GetType() == typeof(ClassSelectButton))
        {
            ClassSelectButton selectedButton = button as ClassSelectButton;
            OnCharacterSelected(selectedButton.GetClassDescription());
        }
        else
        {
            throw new Exception("Class Selection Handler recieved a non-character selector button");
        }
    }

}
