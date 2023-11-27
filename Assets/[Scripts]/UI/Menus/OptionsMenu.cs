using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;


    private void Start()
    {
        CloseMenu();
    }

    public void CloseMenu()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }     
    }

    public void OpenMenu()
    {
        if (!menu.activeInHierarchy)
        {
            menu.SetActive(true);
        }
    }
}
