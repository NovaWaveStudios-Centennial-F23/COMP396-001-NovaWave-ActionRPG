using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // Need to add more specific item types
    // also marge with gear types from gearSO?
    Consumable,
    Misc,

    // gear for testing(should be removed)
    Wand,
    Staff,
    Shield,
    Helmet,
    Chestplate,
    Gloves,
    Boots
}

// Change this later
[CreateAssetMenu(fileName = "New Item Object", menuName = "ScriptableObejcts/Create New Item Object")]
public class ItemSO : ScriptableObject
{
    // Check properties for all type of items
    public Sprite icon;
    public bool stackable;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;

    // Constructer
    public Item()
    {
        Name = "";
        Id = -1;
    }

    public Item(ItemSO item)
    {
        Name = item.name;
        Id = item.data.Id;  // change here to get instance id?
    }
}