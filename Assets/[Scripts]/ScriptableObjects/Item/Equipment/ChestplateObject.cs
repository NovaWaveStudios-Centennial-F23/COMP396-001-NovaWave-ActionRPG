using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chestplate Object", menuName = "ScriptableObejcts/Item/Create New Chestplate Object")]

public class ChestplateObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Chestplate;
    }
}
