using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wand Object", menuName = "ScriptableObejcts/Item/Create New Wand Object")]

public class WandObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Wand;
    }
}
