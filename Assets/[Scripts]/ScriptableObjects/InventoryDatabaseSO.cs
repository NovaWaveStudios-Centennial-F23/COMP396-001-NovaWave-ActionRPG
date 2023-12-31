/*
    Author: Yusuke Kuroki

    This SO is used to store all the items in the game.
    You can add new items into this SO then run UpdateID() to update the id of the items from inspector(click 3 dots).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Database Object", menuName = "ScriptableObejcts/Create New InventoryDB Object")]
public class InventoryDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public GearSO[] GearObjects;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < GearObjects.Length; i++)
        {
            if (GearObjects[i].data.Id != i)
            {
                GearObjects[i].data.Id = i;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize() {}
    
}
