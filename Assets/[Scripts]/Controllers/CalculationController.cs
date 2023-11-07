using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(skillTreeModifiers.Keys.Count);

        //Add stats to an active skill scriptable object
        foreach (Stats s in skillTreeModifiers.Values)
        {
            if (!skill.allStats.Contains(s))
            {
                skill.allStats.Add(s);
            }
            skill.allStats.Find(x => x.stat == s.stat).minValue = s.minValue;
            skill.allStats.Find(x => x.stat == s.stat).maxValue = s.maxValue;
        }

        for (int i = 0; i < skill.allStats.Count; i++)
        {
            switch (skill.allStats[i].stat)
            {
                case Stats.Stat.SkillDamage:
                    CalculateSkillDamage(skill);
                    break;
                /*case Stats.Stat.Cooldown:
                    PercentageSubtraction(skill, Stats.Stat.Cooldown, Stats.Stat.CooldownReductionP);
                    break;
                case Stats.Stat.ManaCost:
                    PercentageSubtraction(skill, Stats.Stat.ManaCost, Stats.Stat.ManaCostRecutionP);
                    break;*/
               /* case Stats.Stat.CastTime:
                    PercentageSubtraction(skill, Stats.Stat.CastTime, Stats.Stat.CastSpeedP);
                    break;*/
                default:
                    break;
            }
        }
    }

    private void SetModifiers(string skillTree)
    {
        playerModifiers = StatsController.Instance.GetAllPlayerModifiers();
        skillTreeModifiers = SkillTreeController.instance.GetModifiers(skillTree);
    }

    private void CalculateSkillDamage(ActiveSkillSO skill)
    {
        // Calculate base damage of the skill [Should be changed to skill damage]
        float skillDamage = skillTreeModifiers[Stats.Stat.SkillDamage].minValue + (skillTreeModifiers[Stats.Stat.SkillDamage].maxValue / 100) * playerModifiers[Stats.Stat.BaseDamage].minValue;

        // Check for the elemental damage stat
        Enum element = skill.damageType;
        string str = element.ToString() + "DamageP";
        Stats.Stat stat;
        Enum.TryParse(str, out stat);

        // Additional elemental damage
        //float elementalDamage = (playerModifiers[stat].minValue / 100) * playerModifiers[Stats.Stat.BaseDamage].minValue;
        //float totalDamage = skillDamage + elementalDamage;

        // [Should be changed to skill damage]
        skill.allStats.Find(x => x.stat == Stats.Stat.SkillDamage).minValue = skillDamage;
        skill.allStats.Find(x => x.stat == Stats.Stat.SkillDamage).maxValue = skillTreeModifiers[Stats.Stat.SkillDamage].maxValue;
    }

    private void PercentageSubtraction(SkillSO skill, Stats.Stat mainStat, Stats.Stat subStat)
    {
        float skillCooldown = (1 - playerModifiers[subStat].minValue) * skillTreeModifiers[mainStat].minValue;

        skill.allStats.Find(x => x.stat == mainStat).minValue = skillCooldown;
    }

    private void PercentageAddition(SkillSO skill, Stats.Stat mainStat, Stats.Stat subStat)
    {
        float skillCooldown = (1 + playerModifiers[subStat].minValue) * skillTreeModifiers[mainStat].minValue;

        skill.allStats.Find(x => x.stat == mainStat).minValue = skillCooldown;
    }

    public float DamageTaken()
    {


        return 0;
    }
}