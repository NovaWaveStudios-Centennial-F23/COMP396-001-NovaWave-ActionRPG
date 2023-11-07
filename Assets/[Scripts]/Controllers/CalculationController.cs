using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using static SkillTreeController;

public class CalculationController : MonoBehaviour
{
    private static CalculationController instance;
    public static CalculationController Instance { get { return instance; } }

    private Dictionary<Stats.Stat, Stats> playerModifiers;
    private Dictionary<Stats.Stat, Stats> skillTreeModifiers;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        skillTreeModifiers = new Dictionary<Stats.Stat, Stats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalculateSkillStats(string skillTree, ActiveSkillSO skill)
    {
        SetModifiers(skillTree);
        
        skill.allStats = new List<Stats>(skillTreeModifiers.Values);        

        for (int i = 0; i < skill.allStats.Count; i++)
        {
            switch (skill.allStats[i].stat)
            {
                case Stats.Stat.Cooldown:
                    PercentageSubtraction(skill, Stats.Stat.Cooldown, Stats.Stat.CooldownReductionP);
                    break;
                case Stats.Stat.ManaCost:
                    PercentageSubtraction(skill, Stats.Stat.ManaCost, Stats.Stat.ManaCostRecutionP);
                    break;
                case Stats.Stat.CastTime:
                    PercentageSubtraction(skill, Stats.Stat.CastTime, Stats.Stat.CastSpeedP);
                    break;
                case Stats.Stat.CritRateP:
                    Addition(skill, Stats.Stat.CritRateP);
                    break;
                case Stats.Stat.CritDamageP:
                    Addition(skill, Stats.Stat.CritDamageP);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetModifiers(string skillTree)
    {
        playerModifiers = StatsController.Instance.GetAllPlayerModifiers();

        // Creating a deep copy of the modifiers dictionary
        foreach (var kvp in SkillTreeController.instance.GetModifiers(skillTree))
        {
            Stats.Stat key = kvp.Key;
            Stats value = new Stats
            {
                stat = kvp.Value.stat,
                minValue = kvp.Value.minValue,
                maxValue = kvp.Value.maxValue
            };
            skillTreeModifiers[key] = value;
        }
    }

    public void CalculateSkillDamage(ActiveSkillSO skill)
    {
        // Calculate base damage of the skill
        float skillDamage = skillTreeModifiers[Stats.Stat.SkillDamage].minValue + (skillTreeModifiers[Stats.Stat.SkillDamage].maxValue / 100) * playerModifiers[Stats.Stat.BaseDamage].minValue;

        // Check for the elemental damage stat
        Enum element = skill.damageType;
        string str = element.ToString() + "DamageP";
        Stats.Stat stat;
        Enum.TryParse(str, out stat);

        // Additional elemental damage
        float elementalDamage = ((playerModifiers[stat].minValue + skillTreeModifiers[stat].minValue) / 100) * playerModifiers[Stats.Stat.BaseDamage].minValue;
        float totalDamage = skillDamage + elementalDamage;

        // Apply calculated damage
        skill.allStats.Find(x => x.stat == Stats.Stat.SkillDamage).minValue = totalDamage;
        skill.allStats.Find(x => x.stat == Stats.Stat.SkillDamage).maxValue = skillTreeModifiers[Stats.Stat.SkillDamage].maxValue;
    }

    private void PercentageSubtraction(SkillSO skill, Stats.Stat mainStat, Stats.Stat subStat)
    {
        float value = (1 - playerModifiers[subStat].minValue) * skillTreeModifiers[mainStat].minValue;

        skill.allStats.Find(x => x.stat == mainStat).minValue = value;
    }

    private void PercentageAddition(SkillSO skill, Stats.Stat mainStat, Stats.Stat subStat)
    {
        float value = (1 + playerModifiers[subStat].minValue) * skillTreeModifiers[mainStat].minValue;

        skill.allStats.Find(x => x.stat == mainStat).minValue = value;
    }

    private void Addition(SkillSO skill, Stats.Stat mainStat)
    {
        float value = skillTreeModifiers[mainStat].minValue + playerModifiers[mainStat].minValue;

        skill.allStats.Find(x => x.stat == mainStat).minValue = value;
    }

    public float DamageOutput(ActiveSkillSO skill)
    {
        float damage = skill.allStats.Find(x => x.stat == Stats.Stat.SkillDamage).minValue;

        // Check for critical hit
        bool crit = false;
        float random = UnityEngine.Random.value;
        if (random <= skill.allStats.Find(x => x.stat == Stats.Stat.CritRateP).minValue / 100)
        {
            crit = true;
        }    

        if (crit)
        {
            damage *= (1 + skill.allStats.Find(x => x.stat == Stats.Stat.CritDamageP).minValue / 100);
            Debug.Log("Crit");
            return damage;
        }
        else
        {
            return damage;
        }
    }
}