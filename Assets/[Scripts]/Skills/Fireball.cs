using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    public SkillSO fireball;
    public Vector3 direction;
    public float damage;
    public List<Stats> fireballStats;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.stats[4].statValue);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        damage = Random.Range(fireball.stats[0].statValue, fireball.stats[1].statValue);
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Cooldown());
    }

    void Update()
    {
        ShootFireball();
    }

    private void ShootFireball()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * fireball.stats[7].statValue;
    }

    private void ApplyBurning()
    {
        if (fireball.stats[9].statValue == 1)
        {
            //do burning
        }
    }
}
