using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : Skill
{
    public SkillSO fireball;
    public Vector3 direction;
    public float damage;
    public List<Stats> fireballStats;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.stats[4].minValue);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        damage = Random.Range(fireball.stats[0].minValue , fireball.stats[1].minValue);
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
        rb.velocity = transform.forward * fireball.stats[7].minValue;
    }

    private void ApplyBurning()
    {
        if (fireball.stats[9].minValue == 1)
        {
            //do burning
        }
    }
}
