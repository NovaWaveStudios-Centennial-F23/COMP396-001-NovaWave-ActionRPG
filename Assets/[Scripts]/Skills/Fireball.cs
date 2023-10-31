using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : Skill
{
    public ActiveSkillSO fireball;
    public Vector3 direction;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Cooldown());
    }

    void Update()
    {
        ShootFireball();
    }

    private void ShootFireball()
    {
        transform.forward = (direction * fireball.allStats.Find(x => x.stat == Stats.Stat.Range).minValue) - transform.position;
        rb.velocity = transform.forward * fireball.allStats.Find(x => x.stat == Stats.Stat.ProjectileSpeed).minValue;
    }

    private void ApplyBurning()
    {
        if (fireball.allStats[9].minValue == 1)
        {
            //do burning
        }
    }
}
