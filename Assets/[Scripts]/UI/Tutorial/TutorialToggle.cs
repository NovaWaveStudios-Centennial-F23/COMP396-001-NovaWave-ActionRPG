using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggle : MonoBehaviour
{
    readonly string TUTORIAL_KEY = "ShowTutorial";
    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        bool showTutorial = ConvertToBool(PlayerPrefs.GetInt(TUTORIAL_KEY, 1));
        toggle = GetComponent<Toggle>();
        toggle.isOn = showTutorial;
    }

    public void OnToggleChanged()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(TUTORIAL_KEY, 0);
        }
    }


    private bool ConvertToBool(int value)
    {

        if (value == 0)
        {
            return false;
        }
        else if (value == 1)
        {
            return true;
        }
        else {

            throw new ArgumentOutOfRangeException($"Cannot covert {value} into bool");
        }

    }

}
