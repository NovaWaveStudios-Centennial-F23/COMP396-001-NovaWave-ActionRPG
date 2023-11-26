using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInventory : MonoBehaviour
{
    // public void OnTriggerEnter(Collider other)
    // {
    //     var groundItem = other.GetComponent<GroundedItem>();
    //     if (groundItem)
    //     {
    //         Item _item = new Item(groundItem.item);
    //         if (inventory.AddItem(_item, 1))
    //         {
    //             Destroy(other.gameObject);
    //         }
    //     }
    // }

    public GameObject inventoryUI;

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    // ========== Add below properties and methods to player script ==========
    public InventorySO inventory;
    GameObject clickedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnItemClicked();
        }
    }

    private void OnItemClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            
            // get parent object(groundedItem) from clicked object
            clickedObject = hit.collider.gameObject.transform.parent.gameObject;

            var groundItem = clickedObject.GetComponent<GroundedItem>();
            if (groundItem)
            {
                Item _item = new Item(groundItem.itemSO);
                if (inventory.AddItem(_item, 1))
                {
                    Destroy(clickedObject);
                }
            }
        }
    }

    // that part might be unnecessary
    public InventorySO equipment;

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }

    // ========== Add above properties and methods to player script ==========
}