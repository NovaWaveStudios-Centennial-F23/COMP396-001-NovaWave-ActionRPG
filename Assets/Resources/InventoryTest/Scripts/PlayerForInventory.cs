using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInventory : MonoBehaviour
{
    // Player should click items to pickup(= no need method for player script. should make into item behaviour script)
    public void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<ItemBehaviour>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    // ========== Add below properties and methods to player script ==========
    // ...or maybe it is not needed.
    public InventorySO inventory;
    public InventorySO equipment;

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }

    // ========== Add above properties and methods to player script ==========
}
