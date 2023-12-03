/*
    Author: Yusuke Kuroki

    This script is SO for inventory and classes for managing inventory.
    inventory SO is made as main inventory and equipment.
    The SO for equipment need to set allowed items to prevent wrong item to be equipped.

    Tasks:
    - check the slot is left/right slot and one of slot has double handed weapon
    - need to get left/right slot
    - return false if item type is double handed
*/

using System.Diagnostics;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using System;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum InterfaceType
{
    Inventory,
    Equipment
}

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "ScriptableObejcts/Create New Inventory Object")]
public class InventorySO : ScriptableObject
{
    public InventoryDatabaseSO database;
    public InterfaceType interfaceType;
    public Inventory Container;
    public InventorySlot[] GetSlots 
    {
        get 
        {
            return Container.Slots;
        }
    }

    public bool AddItem(GearInfo _gearInfo, int _amount)
    {
        if (EmptySlotCount <= 0)
        {
            return false;
        }

        InventorySlot slot = FindItemOnInventory(_gearInfo);
        if (!database.GearObjects[_gearInfo.Id].stackable || slot == null)
        {
            SetEmptySlot(_gearInfo, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].gearInfo.Id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(GearInfo _gearInfo)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].gearInfo.Id == _gearInfo.Id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }

    public InventorySlot SetEmptySlot(GearInfo _gearInfo, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].gearInfo.Id <= -1)
            {
                GetSlots[i].UpdateSlot(_gearInfo, _amount);
                return GetSlots[i];
            }
        }
        // Set up UI to show inventory is full
        return null;
    }

    // item1 = slot
    // item2 = mouse
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        // check the slot is left/right slot and one of slot has double handed weapon
        // need to get left/right slot

        if (item2.CanPlaceInSlot(item1.GearObject) && item1.CanPlaceInSlot(item2.GearObject))
        {
            InventorySlot temp = new InventorySlot(item2.gearInfo, item2.amount);
            item2.UpdateSlot(item1.gearInfo, item1.amount);
            item1.UpdateSlot(temp.gearInfo, temp.amount);
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

//
// Inventory class
//
[System.Serializable]
public class Inventory
{
    public static int numberOfSlots = 48;
    public InventorySlot[] Slots = new InventorySlot[numberOfSlots];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveGear();
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

//
// InventorySlot class
//
[System.Serializable]
public class InventorySlot
{
    public GearSO.GearType[] AllowedSlots = new GearSO.GearType[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public GearInfo gearInfo = new GearInfo();
    public int amount;

    public GearSO GearObject
    {
        get
        {
            if (gearInfo.Id >= 0)
            {
                return parent.inventory.database.GearObjects[gearInfo.Id];
            }
            return null;
        }
    }

    // Constructor
    public InventorySlot()
    {
        UpdateSlot(new GearInfo(), 0);
    }

    public InventorySlot(GearInfo _gearInfo, int _amount)
    {
        UpdateSlot(_gearInfo, _amount);
    }

    // Functions
    public void UpdateSlot(GearInfo _gearInfo, int _amount)
    {
        if (OnBeforeUpdate != null)
        {
            OnBeforeUpdate.Invoke(this);
        }

        gearInfo = _gearInfo;
        amount = _amount;

        if (OnAfterUpdate != null)
        {
            OnAfterUpdate.Invoke(this);
        }
    }

    public void RemoveGear()
    {
        UpdateSlot(new GearInfo(), 0);
    }

    public void AddAmount(int value)
    {
        UpdateSlot(gearInfo, amount += value);
    }

    public bool CanPlaceInSlot(GearSO _gearObject)
    {
        if (AllowedSlots.Length <= 0 || _gearObject == null || _gearObject.data.Id < 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedSlots.Length; i++)
        {
            // return false if item type is double handed here

            if (_gearObject.gearType == AllowedSlots[i])
            {
                return true;
            }
        }
        return false;
    }
}