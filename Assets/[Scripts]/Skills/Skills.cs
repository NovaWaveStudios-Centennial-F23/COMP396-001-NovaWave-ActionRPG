using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skills : MonoBehaviour
{
    [SerializeField] private IgniteScriptableObject ignite;

    // Start is called before the first frame update
    void Start()
    {
        ignite = Resources.Load<IgniteScriptableObject>("Skills/Ignite/Ignite1");
        Debug.Log(ignite.damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
