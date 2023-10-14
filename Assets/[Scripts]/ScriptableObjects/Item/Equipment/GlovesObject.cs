using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gloves Object", menuName = "ScriptableObejcts/Item/Create New Gloves Object")]

public class GlovesObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Gloves;
    }
}
