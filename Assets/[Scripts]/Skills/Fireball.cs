using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fireball : Skill
{
    public Vector3 direction;

    private Rigidbody rb;
    private float cooldown;
    private List<GameObject> enemies;
    private SphereCollider AOE;

    void Start()
    {
        SetInitialValues();
    }

    void Update()
    {
        CalculateCooldown();
    }

    void FixedUpdate()
    {        
        FireballProjectile();
    }

    private void SetInitialValues()
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

    private void FireballProjectile()
    {
        transform.forward = direction;        
        rb.velocity = transform.forward * skillSO.allStats.Find(x => x.stat == Stats.Stat.ProjectileSpeed).minValue;
    }

    private void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        SkillsController.Instance.SetSkillCooldown(nameof(Fireball), cooldown);
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
