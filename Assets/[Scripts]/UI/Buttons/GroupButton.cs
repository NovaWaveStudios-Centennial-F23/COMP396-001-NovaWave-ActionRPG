/***Created by Han Bi
 * A simple button that has click events
 * The button will be highlighted when selected
 * This type of button will not allow user to deselect it
 * will turn off when another button in the same group is selected
 */
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GroupButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Group Button")]    
    [SerializeField]
    protected Image background;

    [SerializeField]
    protected Image icon;

    protected bool isActive;

    //broadcast event when button is changed to active
    public event Action<GroupButton> OnStateActive = delegate { };


    private void Start()
    {
        SetActive(false);
        UpdateAppearance();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(!isActive)
        {
            isActive = true;
            OnStateActive(this);
        }

        UpdateAppearance();
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        UpdateAppearance();
    }

    protected virtual void UpdateAppearance()
    {
        if(isActive)
        {
            background.color = UIConstants.selectedSkillOutline;
            icon.color = UIConstants.selectedSkillColor;
        }
        else
        {
            background.color = UIConstants.deselectedSkillOutline;
            icon.color = UIConstants.deselectedSkillColor;
        }
    }



}
