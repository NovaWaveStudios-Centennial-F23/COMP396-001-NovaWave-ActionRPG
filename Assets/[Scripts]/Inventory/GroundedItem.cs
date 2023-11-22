/*
    Author: Yusuke Kuroki
    
    This script is for items that are on the ground.
    (Please change Gizmos setting as you like if you feel uncomfortable)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundedItem : MonoBehaviour
{
    public ItemSO itemSO;

    void Start()
    {
        // Instantiate mesh as a chilf from itemSO
        GameObject childObject = Instantiate(itemSO.groundPrefab, this.transform);
        // set is trigger on
        childObject.GetComponent<Collider>().isTrigger = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        // Gizmos.DrawMesh(this.itemObj, this.transform.position, Quaternion.identity, Vector3.one);
    }
}
