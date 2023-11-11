using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;
using static StatFinder;

public class LightningStrike : Skill
{
    private ParticleSystem ps, psChild;

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
            yield return new WaitForSeconds(0.2f);
            damage = CalculationController.Instance.DamageOutput(skillSO);
            foreach (GameObject g in enemies)
            {
                g.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        SetIntitialValues();
        SetParticleSystem();
        StartCoroutine(DealDamage());
        StartCoroutine(Duration());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCooldown();
    }

    public override void SetIntitialValues()
    {
        AOE = GetComponent<SphereCollider>();
        enemies = new List<GameObject>();
        cooldown = FindStat(skillSO, Stat.Cooldown).minValue;
        aoe = FindStat(skillSO, Stat.AOE).minValue;
        AOE.radius = aoe / 10;
    }


    public override void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        SkillsController.Instance.SetSkillCooldown(nameof(LightningStrike), cooldown);

        if (cooldown <= -0.1)
        {
            Destroy(gameObject);
        }
    }

    private void SetParticleSystem()
    {
        // Get PS main and emission
        ps = GetComponent<ParticleSystem>();
        psChild = transform.Find("Smoke").gameObject.GetComponent<ParticleSystem>();
        psChild.transform.localScale = new Vector3(aoe / 50, 0.5f, aoe / 50);

        var main = ps.main;
        var emission = ps.emission;
        var shape = ps.shape;
        var mainChild = psChild.main;

        main.duration = FindStat(skillSO, Stat.Duration).minValue * 5;
        emission.SetBurst(0, new ParticleSystem.Burst(2.0f, 1, 1, (int) main.duration * 4, 0.3f));
        shape.radius = aoe / 10;
        mainChild.duration = FindStat(skillSO, Stat.Duration).minValue + 1;

        ps.Play(true);
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

    public override void MovementBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
