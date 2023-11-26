using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using static Stats;
using static StatFinder;
using Mirror;

public class Fireball : Skill
{
    public Vector3 direction;
    private Rigidbody rb;

    public override IEnumerator Duration()
    {
        yield return new WaitForSeconds(FindStat(skillSO, Stat.Duration).minValue + 0.1f);
        gameObject.layer = 6;
    }

    void Start()
    {
        if (isServer)
        {
            SetIntitialValues();
            StartCoroutine(Duration());
        }
        
    }

    void Update()
    {
        CalculateCooldown();
    }

    void FixedUpdate()
    {
        if (isServer)
        {
            MovementBehaviour();
        }
        
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
        //direction = SkillsController.Instance.mousePosition - transform.position;
        cooldown = FindStat(skillSO, Stat.Cooldown).minValue;
        AOE.radius = FindStat(skillSO, Stat.AOE).minValue / 10;
    }

    public override void MovementBehaviour()
    {
        transform.forward = direction;
        rb.velocity = transform.forward * FindStat(skillSO, Stat.ProjectileSpeed).minValue;
    }

    public override void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        SkillsController.Instance.SetSkillCooldown(nameof(Fireball), cooldown);

        if (cooldown <= -0.1 && gameObject.layer == 6)
        {
            DestroySelf();
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        //damage will be handled on server side to avoid double counting and other issues
        if (!isServer)
        {
            return;
        }
        // Damage Output
        //damage = CalculationController.Instance.DamageOutput(skillSO);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Debug.Log(damage);
            foreach (GameObject g in enemies)
            {
                if (g != other.gameObject)
                {
                    g.gameObject.GetComponent<Health>().TakeDamage(damage / 2);
                    Debug.Log(damage / 2);
                }
            }
        }

        // Make Game Object invisible
        gameObject.layer = 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
        {
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isServer) 
        {
            return;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
