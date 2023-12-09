using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public InventorySO inventory;
    public InventorySO equipment;

    void Start()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }
}
