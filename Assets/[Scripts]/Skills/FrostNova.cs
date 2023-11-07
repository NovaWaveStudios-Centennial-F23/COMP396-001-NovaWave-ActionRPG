using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNova : Skill
{
    IEnumerator Duration()
    {
        yield return new WaitForSeconds(skillSO.allStats.Find(x => x.stat == Stats.Stat.Duration).minValue + 0.1f);

        gameObject.layer = 6;
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
            //Debug.Log("damage");
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetIntitialValues();
        StartCoroutine(DealDamage());
        StartCoroutine(Duration());
    }

    // Update is called once per frame
    void Update()
    {
        MovementBehaviour();
        CalculateCooldown();
    }

    public override void SetIntitialValues()
    {
        AOE = GetComponent<SphereCollider>();
        enemies = new List<GameObject>();
        cooldown = skillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue;
        AOE.radius = skillSO.allStats.Find(x => x.stat == Stats.Stat.AOE).minValue / 10;
    }

    public override void MovementBehaviour()
    {
        transform.position = SkillsController.Instance.player.transform.position;
    }

    public override void CalculateCooldown()
    {
        // Live cooldown counter
        cooldown -= Time.deltaTime;
        SkillsController.Instance.SetSkillCooldown(nameof(FrostNova), cooldown);

        if (cooldown <= -0.1)
        {
            Destroy(gameObject);
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
