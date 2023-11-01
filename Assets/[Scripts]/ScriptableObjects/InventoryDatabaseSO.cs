using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Database Object", menuName = "ScriptableObejcts/Create New InventoryDB Object")]
public class InventoryDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] Items;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].data.Id != i)
            {
                Items[i].data.Id = i;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize() {}
    
}
