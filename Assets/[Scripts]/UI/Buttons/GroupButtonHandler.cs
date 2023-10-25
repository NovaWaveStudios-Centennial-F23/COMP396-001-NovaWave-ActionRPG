using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupButtonHandler : MonoBehaviour
{
    [SerializeField]
    List<GroupButton> buttons;

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        foreach (GroupButton button in buttons)
        {
            if (button != null)
            {
                button.OnStateActive += HandleButtonStateChange;
            }

        }
    }

    protected virtual void HandleButtonStateChange(GroupButton button)
    {
        foreach(GroupButton btn in buttons)
        {
            if(btn != button)
            {
                btn.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GroupButton button in buttons)
        {
            if(button != null)
            {
                button.OnStateActive -= HandleButtonStateChange;
            }
            
        }
    }


}
