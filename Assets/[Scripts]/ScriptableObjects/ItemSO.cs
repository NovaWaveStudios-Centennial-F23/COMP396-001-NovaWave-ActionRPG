using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // Need to add more specific item types
    // and marge with gear types from gearSO
    Consumable,
    Misc,
    Default,
    Wand,
    Staff,
    Shield,
    Helmet,
    Chestplate,
    Gloves,
    Boots
}

public abstract class ItemSO : ScriptableObject
{
    // Check properties for all type of items
    public Sprite icon;
    public ItemType itemType;
    public int Id;
    [TextArea(15, 20)]
    public string description;

}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    // Constructer
    public Item()
    {
        Name = "";
        Id = -1;
    }

    public Item(ItemSO item)
    {
        Name = item.name;
        Id = item.Id;
    }
}