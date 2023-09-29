using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ignite : MonoBehaviour
{
    public IgniteScriptableObject ignite;
    public Vector3 direction;

    [SerializeField] private int damage;

    private Rigidbody rb;
    private float speed = 20f;
   
    void Start()
    {
        damage = ignite.damage;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * speed;
    }
}
