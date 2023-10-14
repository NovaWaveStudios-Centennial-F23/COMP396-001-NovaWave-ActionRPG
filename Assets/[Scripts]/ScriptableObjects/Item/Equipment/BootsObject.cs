using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boots Object", menuName = "ScriptableObejcts/Item/Create New Boots Object")]

public class BootsObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Boots;
    }
}
