using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventorySO inventory;
    GameObject clickedObject;

    // Spawn item on ground from enemy
    // Spawn gear on ground from enemy
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnItemClicked();
        }
    }

    // pick up item from ground
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
}
