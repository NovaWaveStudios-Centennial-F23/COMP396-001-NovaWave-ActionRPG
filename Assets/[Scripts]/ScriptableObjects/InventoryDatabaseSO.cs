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
    public GearSO[] Gears;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < Gears.Length; i++)
        {
            if (Gears[i].data.Id != i)
            {
                Gears[i].data.Id = i;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize() {}
    
}
