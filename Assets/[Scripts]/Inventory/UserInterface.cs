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
    // player object
    public MyPlayer player;

    // Inventory Scriptable object
    public InventorySO inventory;

    // Display properties
    public Dictionary<GameObject, InventorySlot> itemDisplayed = new Dictionary<GameObject, InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public abstract void CreateSlots();

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].icon;
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

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        // Add event to event trigger
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
        player.mouseItem.hoverObj = obj;
        if (itemDisplayed.ContainsKey(obj))
        {
            player.mouseItem.hoverItem = itemDisplayed[obj];
        }
    }

    public void OnExit(GameObject obj)
    {
        // Delete hover mouse item
        player.mouseItem.hoverObj = null;
        player.mouseItem.hoverItem = null;
    }

    public void OnEnterInterface(GameObject obj)
    {
        // set up ui
        player.mouseItem.ui = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        // de-set up ui
        player.mouseItem.ui = null;
    }

    public void OnDragStart(GameObject obj)
    {
        // Get item from inventory
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(30, 30);
        mouseObject.transform.SetParent(transform.parent);
        if (itemDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemDisplayed[obj].ID].icon;
            img.raycastTarget = false;
        }
        player.mouseItem.obj = mouseObject;
        player.mouseItem.item = itemDisplayed[obj];
    }

    public void OnDragEnd(GameObject obj)
    {
        var itemOnMouse = player.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObj = itemOnMouse.hoverObj;
        var GetItemObject = inventory.database.GetItem;

        if(itemOnMouse.ui != null)
        {
            // Item move event
            if (mouseHoverObj)
            {
                // Check if mouse hover item can place in equipment slot
                if (mouseHoverItem.CanPlaceInSlot(GetItemObject[itemDisplayed[obj].ID]) 
                    && (mouseHoverItem.item.Id <= -1 || (mouseHoverItem.item.Id >= 0 && itemDisplayed[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.Id]))))
                {
                    // Move/Swap item
                    inventory.MoveItem(itemDisplayed[obj], mouseHoverItem.parent.itemDisplayed[itemOnMouse.hoverObj]);
                }
            }
        }
        else
        {
            UnityEngine.Debug.Log("Drop item");
            // Remove dragged item
            inventory.RemoveItem(itemDisplayed[obj].item);
        }
        // Destroy mouse item
        Destroy(itemOnMouse.obj);
        itemOnMouse.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        // Move item icon to mouse position
        if (player.mouseItem.obj != null)
        {
            player.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

// Item on mouse
public class MouseItem
{
    public UserInterface ui;

    public GameObject obj;
    public InventorySlot item;

    // Hover item
    public GameObject hoverObj;
    public InventorySlot hoverItem;
}
