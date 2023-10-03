using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public FireballScriptableObject fireball;
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
        damage = Mathf.Round(28 * Mathf.Log((600 * fireball.level) + 650) - 190);
        Debug.Log(damage);
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Cooldown());
    }

    void Update()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * fireball.projectileSpeed;
    }
}
