using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Helmet Object", menuName = "ScriptableObejcts/Item/Create New Helmet Object")]

public class HelmetObject : ItemSO
{
    // Add properties here if needed
    // This will be used for test purpose?
    
    public void Awake()
    {
        itemType = ItemType.Helmet;
    }
}
