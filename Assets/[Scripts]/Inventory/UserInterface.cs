/*
    Author: Yusuke Kuroki
    
    This script is main inventory script. It cannot be use directory, but can be extended.

    Tasks:
    - Check if item is stackable and separate if needed(or separate right/left click)
    - Check if item is stackable then remove item all or one by one
*/

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    // Inventory Scriptable object
    public InventorySO inventory;

    // Display properties
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    // Value for slot size
    public int xSlot = 100;
    public int ySlot = 100;

    // properties for dropping item
    public GameObject player;
    public PickableObject groundedItem;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    private void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.gearInfo.Id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.GearObject.icon;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
        }
        else
        {
            // empty slot
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     slotsOnInterface.UpdateSlotDisplay();
    // }

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        // Add events to event trigger
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var EventTrigger = new EventTrigger.Entry();
        EventTrigger.eventID = type;
        EventTrigger.callback.AddListener(action);
        trigger.triggers.Add(EventTrigger);
    }

    // Mouse events
    public void OnEnter(GameObject obj)
    {
        // copy item to hover mouse item
        MouseData.slotHoveredOver = obj;

        // show tooltip if slot is not empty
        if (slotsOnInterface[obj].gearInfo.Id >= 0)
        {
            ToolTipController.Instance.ShowGearTooltip(slotsOnInterface[obj].GearObject);
        }
    }

    public void OnExit(GameObject obj)
    {
        // Delete hover mouse item
        MouseData.slotHoveredOver = null;
        // close tooltip
        ToolTipController.Instance.CloseTooltips();
    }

    public void OnEnterInterface(GameObject obj)
    {
        // set ui on mouse cursor
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        // Remove ui from mouse cursor
        MouseData.interfaceMouseIsOver = null;
    }

    public void OnDragStart(GameObject obj)
    {
        // Copy item data to mouse cursor
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].gearInfo.Id >= 0)
        {
        // Get item from inventory
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(xSlot, ySlot);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].GearObject.icon;
            img.raycastTarget = false;
        }

        return tempItem;
    }

    public void OnDragEnd(GameObject obj)
    {
        /* Tasks:
            - Check if item is stackable and separate if needed(or separate right/left click)
            - Check if item is stackable then remove item all or one by one
        */

        // Destroy dragged item
        Destroy(MouseData.tempItemBeingDragged);

        if (MouseData.interfaceMouseIsOver == null)
        {
            // Instantiate item to field
            GearSO droppedItemData = slotsOnInterface[obj].GearObject;
            groundedItem.gearSO = droppedItemData;
            Instantiate(groundedItem, player.transform.position, Quaternion.identity);

            // Remove dragged item from inventory
            slotsOnInterface[obj].RemoveGear();
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    }

    public void OnDrag(GameObject obj)
    {
        // Move item icon to mouse position
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

// Item on mouse
public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;

    // Hover item
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.gearInfo.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.GearObject.icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                // empty slot
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}