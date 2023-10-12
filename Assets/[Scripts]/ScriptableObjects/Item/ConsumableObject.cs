using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "ScriptableObejcts/Item/Create New Consumable Object")]

public class ConsumableObject : ItemSO
{
    // Add properties here if needed

    public int value;
    public bool isHealthCure;

    public void Awake()
    {
        itemType = ItemType.Consumable;
    }
}
