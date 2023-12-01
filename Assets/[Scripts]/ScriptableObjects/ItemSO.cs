/*
    Author: Yusuke Kuroki

    This SO is for item/gear creation.
    *Won't be used, gear will be similar to this script.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Misc
}

// Change this later
[CreateAssetMenu(fileName = "New Item Object", menuName = "ScriptableObejcts/Create New Item Object")]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public ItemType itemType;
    public bool stackable;
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
        Id = item.data.Id;
    }
}