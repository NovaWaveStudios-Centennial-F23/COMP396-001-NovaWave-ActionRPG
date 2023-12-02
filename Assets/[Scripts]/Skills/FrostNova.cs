using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;
using static StatFinder;
using Mirror;

public class FrostNova : Skill
{
    private ParticleSystem ps, psChild;

    public GameObject player;

    public override IEnumerator Duration()
    {
        yield return new WaitForSeconds(FindStat(skillSO, Stat.Duration).minValue + 0.1f);

        gameObject.layer = 6;
        AOE.enabled = false;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = 6;
        }
    }
    IEnumerator DealDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            //damage = CalculationController.Instance.DamageOutput(skillSO);
            foreach (GameObject g in enemies)
            {
                g.GetComponent<Health>().TakeDamage(damage);
            }
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            SetIntitialValues();
            StartCoroutine(DealDamage());
            StartCoroutine(Duration());
        }
        SetParticleSystem();

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            MovementBehaviour();
        }
        CalculateCooldown();

    }

    public override void SetIntitialValues()
    {
        AOE = GetComponent<SphereCollider>();
        enemies = new List<GameObject>();
        cooldown = FindStat(skillSO, Stat.Cooldown).minValue;
        aoe = FindStat(skillSO, Stat.AOE).minValue;
        AOE.radius = (0.1f * aoe) + 2;
    }

    public override void MovementBehaviour()
    {
        transform.position = player.transform.position;
    }

    private void SetParticleSystem()
    {
        ps = GetComponent<ParticleSystem>();

        var shape = ps.shape;
        shape.radius = aoe / 10;

        foreach (Transform child in gameObject.transform)
        {
            psChild = child.GetComponent<ParticleSystem>();
            var main = psChild.main;
            if (child.gameObject.name == "ShockWave")
            {
                main.startSize = aoe / 5;
            }
            else
            {
                main.startSize = aoe / 2.5f;
            }
        }

        ps.Play(true);
    }
    public override void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        //SkillsController.Instance.UpdateSkillCooldown(nameof(FrostNova), cooldown);

        if (cooldown <= -0.1)
        {
            DestroySelf();
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                enemies.Add(other.gameObject);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isServer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                enemies.Remove(other.gameObject);
            }
        }
        
    }
}
