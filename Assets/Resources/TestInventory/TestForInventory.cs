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

    // Make slots and then close UI(Inventory needs to be active for generating slots before deactivating)
    void Start()
    {
        inventoryUI.SetActive(false);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}
