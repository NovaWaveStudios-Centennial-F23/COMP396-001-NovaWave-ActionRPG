using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fireball : Skill
{
    public Vector3 direction;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(skillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        direction = SkillsController.Instance.mousePosition;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Cooldown());
    }

    void FixedUpdate()
    {
        ShootFireball();
    }

    private void ShootFireball()
    {
        transform.forward = (direction * skillSO.allStats.Find(x => x.stat == Stats.Stat.Range).minValue) - transform.position;
        rb.velocity = transform.forward * skillSO.allStats.Find(x => x.stat == Stats.Stat.ProjectileSpeed).minValue;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
        }
    }
}
