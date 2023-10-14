using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    // ========== Add below properties and methods to player script ==========
    public InventorySO inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemBehaviour>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);}        
    }

    private void OnApplicationQuit()
    {
        // implement a logic to put same nunber to InventorySlot.
        inventory.Container.Items = new InventorySlot[48];
    }

    // ========== Add above properties and methods to player script ==========
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     UnityEngine.Debug.Log("Save");
        //     inventory.Save();
        // }
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     UnityEngine.Debug.Log("Load");
        //     inventory.Load();
        // }
    }
}
