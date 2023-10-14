using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Staff Object", menuName = "ScriptableObejcts/Item/Create New Staff Object")]

public class StaffObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Staff;
    }
}
