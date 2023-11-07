using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fireball : Skill
{
    private Vector3 direction;
    private Rigidbody rb;

    void Start()
    {
        SetIntitialValues();
    }

    void Update()
    {
        CalculateCooldown();
    }

    void FixedUpdate()
    {        
        MovementBehaviour();
    }

    public override void SetIntitialValues()
    {
        rb = GetComponent<Rigidbody>();

        SphereCollider[] colliders = GetComponents<SphereCollider>();
        foreach (SphereCollider sc in colliders)
        {
            if (sc.isTrigger)
            {
                AOE = sc;
            }
        }

        enemies = new List<GameObject>();
        direction = SkillsController.Instance.mousePosition - transform.position;
        cooldown = skillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue;
        AOE.radius = skillSO.allStats.Find(x => x.stat == Stats.Stat.AOE).minValue / 10;
    }

    public override void MovementBehaviour()
    {
        transform.forward = direction;
        rb.velocity = transform.forward * skillSO.allStats.Find(x => x.stat == Stats.Stat.ProjectileSpeed).minValue;
    }

    public override void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        SkillsController.Instance.SetSkillCooldown(nameof(Fireball), cooldown);

        if (cooldown <= -0.1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Damage Output
        float damage = CalculationController.Instance.DamageOutput(skillSO);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit, Damage: " + damage);

            foreach (GameObject g in enemies)
            {
                Debug.Log(" Damage to each: " + damage / 2);
            }
        }

        // Make Game Object invisible
        gameObject.layer = 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
