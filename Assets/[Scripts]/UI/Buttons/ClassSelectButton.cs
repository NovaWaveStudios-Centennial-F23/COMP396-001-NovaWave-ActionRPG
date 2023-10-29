using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelectButton : GroupButton
{
    [SerializeField]
    GameObject classModel;

    [SerializeField]
    ClassDescriptionSO classDesciption;

    protected override void UpdateAppearance()
    {
        base.UpdateAppearance();
        if (isActive)
        {
            if (!classModel.activeInHierarchy)
            {
                classModel.SetActive(true);
            }
        }
        else if (!isActive)
        {
            classModel.SetActive(false);
        }
    }

    public ClassDescriptionSO GetClassDescription()
    {
        return classDesciption;
    }

    private void OnDisable()
    {
        if (classModel != null)
        {
            classModel.SetActive(false);
        }
    }

}
