/**Created by Han Bi
 * simple script to close target element when clicked
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnDefocus : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    GameObject target;

    public void OnPointerDown(PointerEventData eventData)
    {
        target.SetActive(false);
    }
}
