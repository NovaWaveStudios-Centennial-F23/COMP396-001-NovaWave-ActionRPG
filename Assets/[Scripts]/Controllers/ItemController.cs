using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventorySO inventory;
    public InventoryDatabaseSO database;
    GameObject clickedObject;
    public PickableObject spawnedBase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnItemClicked();
        }
    }

    // pick up item/gear from ground
    private void OnItemClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            // get parent object(groundedItem) from clicked object
            clickedObject = hit.collider.gameObject.transform.parent.gameObject;

            var groundItem = clickedObject.GetComponent<PickableObject>();
            if (groundItem)
            {
                GearInfo _gearInfo = new GearInfo(groundItem.gearSO);
                if (inventory.AddItem(_gearInfo, 1))
                {
                    Destroy(clickedObject);
                }
            }
        }
    }

    // Spawn item on ground from enemy
    // Called when enemy is killed
    public void DropItemOnGround(GearSO gearSO, Vector3 position)
    {
        // UnityEngine.Debug.Log(itemSO);

        /*
            need to :
            - select gearType from db
            - select rairity from db

            stats should be selected from other functions?
        */

        // Instantiate item on field
        spawnedBase.gearSO = gearSO;
        Instantiate(spawnedBase, position, Quaternion.identity);
    }

    // Spawn gear on ground from enemy
    public void DropGearOnGround(GearSO gearSO, Vector3 position)
    {
        UnityEngine.Debug.Log("Gear spawned");
    }

    // Test function for spawning item/gear
    public void SpawnObjectById(int dataBaseId)
    {   
        DropItemOnGround(database.Gears[dataBaseId], new Vector3(1, 1, 0));
    }
}
