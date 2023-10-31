using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupButtonHandler : MonoBehaviour
{
    [SerializeField]
    List<GroupButton> buttons;

    private void Start()
    {
        if(buttons.Count != 0)
        {
            Initialize();
        }
        
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

    public void AddButton(GroupButton button)
    {
        buttons.Add(button);
        button.OnStateActive += HandleButtonStateChange;
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

    private void OnDisable()
    {
        foreach (GroupButton btn in buttons)
        {
            btn.SetActive(false);
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
