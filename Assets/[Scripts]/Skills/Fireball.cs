using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public FireballSO fireball;
    public Vector3 direction;
    public float damage;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.coolDown);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        damage = Random.Range(fireball.minDamage, fireball.maxDamage);
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Cooldown());
    }

    void Update()
    {
        Projectile();
    }

    private void Projectile()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * fireball.speed;
    }
}
