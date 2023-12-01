/*
    Author: Yusuke Kuroki
    
    This script is for items that are on the ground.
    change name to PickableObject
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableObject : MonoBehaviour
{
    public GearSO gearSO;

    void Start()
    {
        // // Instantiate mesh as a child from itemSO
        // GameObject childObject = Instantiate(itemSO.groundPrefab, this.transform);
        // // set is trigger on
        // childObject.GetComponent<Collider>().isTrigger = true;
    }
}
