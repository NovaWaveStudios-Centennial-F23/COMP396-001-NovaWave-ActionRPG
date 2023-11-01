using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemSO item;

    // need method to click item to pickup

    public void OnAfterDeserialize() {}
    public void OnBeforeSerialize() {}
}
