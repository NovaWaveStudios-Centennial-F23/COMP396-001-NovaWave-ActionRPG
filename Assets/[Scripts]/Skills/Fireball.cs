using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public FireballScriptableObject fireball;
    public Vector3 direction;

    
    private int damage;
    private Rigidbody rb;
    private float speed = 20f;

    void Start()
    {
        damage = fireball.damage;
        rb = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * speed;
    }
}
