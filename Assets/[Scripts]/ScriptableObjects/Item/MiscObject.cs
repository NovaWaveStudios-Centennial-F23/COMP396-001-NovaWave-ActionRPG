using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Object", menuName = "ScriptableObejcts/Item/Create New Misc Object")]

public class MiscObject : ItemSO
{
    // Add properties here if needed

    public void Awake()
    {
        itemType = ItemType.Misc;
    }
}
