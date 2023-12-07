using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    private static ItemController instance;
    public static ItemController Instance { get { return instance; } }

   public InventorySO inventory;
    public InventorySO equipment;
    // public InventoryDatabaseSO database;

    // properties for enter/clicked object
    RaycastHit hit = new RaycastHit();
    GameObject targetObject;

    // prefab for spawning
    public PickableObject spawnedBase;
    public GameObject potionObject;

    [SerializeField]
	[Range( 0.0f, 100.0f)]
    public float dropProbability;

    // properties for life potion
    public int lifePotionCount = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

     private void Start()
    {
        // Add slot update functions to equipment slots
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            // equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            UseLifePotion();
        }

        ShowToolTip();
    }

    // pick up gear/potion from ground
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
                print("Potion clicked");
                // add amount of life potion and update UI
                lifePotionCount++;

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
            else if (targetObject.CompareTag("Potion"))
            {
                return;
            }
            else
            {
                // close tooltip
                targetObject = null;
                ToolTipController.Instance.CloseTooltips();
            }
        }
    }

    public void SpawnPotion(Vector3 position)
    {
        float random = Random.Range(0f, 100f);
        if (random <= dropProbability)
        {
            Instantiate(potionObject, position, Quaternion.identity);
        }
    }

    // Use potion for life healing
    public void UseLifePotion()
    {
        if (lifePotionCount > 0)
        {
            // heal player

            // update amount
            lifePotionCount--;
        }
    }

    // Won't be used
    // public void DropObjectOnGroundById(int dataBaseId, Vector3 position)
    // {
    //     // Check if id is invalid
    //     if (dataBaseId < 0 || dataBaseId >= database.GearObjects.Length)
    //     {
    //         return;
    //     }

    //     // Instantiate item on field
    //     spawnedBase.gearSO = database.GearObjects[dataBaseId];
    //     Instantiate(spawnedBase, position, Quaternion.identity);
    // }

    // Spawning gear (this is for testing)
    public void ButtonSpawnTest(int dataBaseId)
    {   
        // DropObjectOnGroundById(dataBaseId, new Vector3(1, 1, 0));
    }

    // private void OnApplicationQuit()
    // {
    //     inventory.Container.Clear();
    //     equipment.Container.Clear();
    // }
}
