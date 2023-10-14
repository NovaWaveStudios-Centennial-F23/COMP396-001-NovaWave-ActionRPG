using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    // Inventory Scriptable object
    public InventorySO inventory;

    // Display properties
    public GameObject inventoryIcon;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    Dictionary<GameObject, InventorySlot> itemDisplayed = new Dictionary<GameObject, InventorySlot>();
    public MouseItem mouseItem = new MouseItem();

    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

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

    public void CreateSlots()
    {
        itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryIcon, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            // Add event for each actions
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            // obj.GetComponentInChildren<Image>().sprite = inventory.Container.Items[i].item.icon;
            // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");
            itemDisplayed.Add(obj, inventory.Container.Items[i]);
        }

    }

    public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
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
        mouseItem.hoverObj = obj;
        if (itemDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemDisplayed[obj];
        }
    }

    public void OnExit(GameObject obj)
    {
        // Delete hover mouse item
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
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
        mouseItem.obj = mouseObject;
        mouseItem.item = itemDisplayed[obj];

    }

    public void OnDragEnd(GameObject obj)
    {
        // Item move event
        if (mouseItem.hoverObj)
        {
            // move/swap item
            inventory.MoveItem(itemDisplayed[obj], itemDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            // remove dragged item
            inventory.RemoveItem(itemDisplayed[obj].item);
        }
        // Destroy mouse item
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        // Move item icon to mouse position
        if (mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}

// Item on mouse
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;

    // Hover item
    public GameObject hoverObj;
    public InventorySlot hoverItem;


}
