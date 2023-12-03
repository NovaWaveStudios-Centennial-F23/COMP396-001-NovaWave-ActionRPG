using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    public InventorySO inventory;
    public InventorySO equipment;
    public InventoryDatabaseSO database;

    // properties for enter/clicked object
    RaycastHit hit = new RaycastHit();
    GameObject targetObject;

    // prefab for spawning gear on ground
    public PickableObject spawnedBase;

    // properties for life portion
    public GameObject textLifePortionUI;    // Assign PortionUI->PotionSlot->Amount
    public int lifePortionCount = 0;

     private void Start()
    {
        // Add slot update functions to equipment slots
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            // equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

        // PortionUI
        textLifePortionUI.GetComponent<TextMeshProUGUI>().text = lifePortionCount.ToString();
    }

    // Won't be used but keep it for now
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.gearInfo == null)
        {
            return;
        }

        // switch (_slot.parent.inventory.interfaceType)
        // {
        //     case InterfaceType.Inventory:
        //         break;
        //     case InterfaceType.Equipment:
        //         // print(string.Concat("Removed ", _slot.gearInfo, " on ", _slot.parent.inventory.interfaceType, ", Allowed Gears: ", string.Join(", ", _slot.AllowedSlots)));

        //         // for (int i = 0; i < equipment.GetSlots.Length; i++)
        //         // {
        //         //     print("OnBeforeSlotUpdate. slot " + i + ": " + equipment.GetSlots[i].gearInfo.Id);
        //         // }

        //         // rmeove gearSO from equipment slots

        //         break;
        //     default:
        //         break;
        // }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.gearInfo == null)
        {
            return;
        }

        switch (_slot.parent.inventory.interfaceType)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                // print(string.Concat("Placed ", _slot.gearInfo, " on ", _slot.parent.inventory.interfaceType, ", Allowed Gears: ", string.Join(", ", _slot.AllowedSlots)));

                List<GearSO> equipped = new List<GearSO>();

                // make list of equipped gearSO from equipment slots
                for (int i = 0; i < equipment.GetSlots.Length; i++)
                {
                    if (equipment.GetSlots[i].gearInfo.Id >= 0)
                    {
                        equipped.Add(equipment.GetSlots[i].GearObject);
                    }
                }

                // send the list to calculater?(for stats)

                // print("number of equipped gear: " + equipped.Count);
                // for (int i = 0; i < equipped.Count; i++)
                // {
                //     print(equipped[i].gearType);
                // }

                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnItemClicked();
        }

        ShowToolTip();
    }

    // pick up item/gear from ground
    private void OnItemClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // get parent object(groundedItem) from clicked object
            targetObject = hit.collider.gameObject.transform.parent.gameObject;

            if (targetObject.CompareTag("GroundedGear"))
            {
                // if clicked object is gear, add it to inventory
                var groundItem = targetObject.GetComponent<PickableObject>();
                if (groundItem)
                {
                    GearInfo _gearInfo = new GearInfo(groundItem.gearSO);
                    if (inventory.AddItem(_gearInfo, 1))
                    {
                        Destroy(targetObject);
                    }
                }
            }
            else if (targetObject.CompareTag("Potion"))
            {
                // add amount of life portion and update UI
                lifePortionCount++;
                textLifePortionUI.GetComponent<TextMeshProUGUI>().text = lifePortionCount.ToString();

                // destroy clicked object
                Destroy(targetObject);
            }
            else 
            {
                // if clicked object is not gear or potion, do nothing
                // UnityEngine.Debug.Log("Clicked object is not gear or potion)");
                return;
            }
        }
    }

    private void ShowToolTip()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // get parent object(groundedItem) from clicked object
            targetObject = hit.collider.gameObject.transform.parent.gameObject;

            if (targetObject.CompareTag("GroundedGear"))
            {
                // show tooltip
                var groundItem = targetObject.GetComponent<PickableObject>();
                ToolTipController.Instance.ShowGearTooltip(groundItem.gearSO);
            }
            else
            {
                // close tooltip
                targetObject = null;
                ToolTipController.Instance.CloseTooltips();
            }
        }
    }

    // Use portion for life healing
    public void UseLifePortion()
    {
        if (lifePortionCount > 0)
        {
            // heal player(use health.cs?)

            // update amount and UI
            lifePortionCount--;
            textLifePortionUI.GetComponent<TextMeshProUGUI>().text = lifePortionCount.ToString();
        }
    }

    // Spawn gear on ground based on id from database
    // Send id from GearSO and position of enemy
    public void DropObjectOnGroundById(int dataBaseId, Vector3 position)
    {
        // Check if id is invalid
        if (dataBaseId < 0 || dataBaseId >= database.GearObjects.Length)
        {
            return;
        }

        // Instantiate item on field
        spawnedBase.gearSO = database.GearObjects[dataBaseId];
        Instantiate(spawnedBase, position, Quaternion.identity);
    }

    // Spawn gear on ground based on rairity?
    // Spawn gear on ground based on gearType?

    // Spawning gear by id(this is for testing)
    public void ButtonSpawnTest(int dataBaseId)
    {   
        DropObjectOnGroundById(dataBaseId, new Vector3(1, 1, 0));
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}
