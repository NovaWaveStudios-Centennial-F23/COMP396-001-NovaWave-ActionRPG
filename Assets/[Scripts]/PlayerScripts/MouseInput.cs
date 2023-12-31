using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPosition;
  
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            mouseInputPosition = hit.point;
            //UnityEngine.Debug.Log(mouseInputPosition);
        }
    }
}
