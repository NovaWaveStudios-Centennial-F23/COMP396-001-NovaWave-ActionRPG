using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : MonoBehaviour
{
    [SerializeField] private IgniteScriptableObject igniteScriptableObejct;

    void Start()
    {
        Debug.Log(igniteScriptableObejct.damage);
    }
}
