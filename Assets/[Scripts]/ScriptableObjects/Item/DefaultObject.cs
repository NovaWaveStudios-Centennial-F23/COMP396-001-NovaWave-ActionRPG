using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "ScriptableObejcts/Item/Create New Default Object")]

public class DefaultObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Default;
    }
}
