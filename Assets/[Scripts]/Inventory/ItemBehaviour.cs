using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemSO item;

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
    }

    // Might not needed
    // Get item icon from child
    public void OnBeforeSerialize()
    {
        // GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
        // EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }

}
