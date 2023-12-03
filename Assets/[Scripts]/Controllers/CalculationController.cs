using System;
using System.Collections.Generic;
using UnityEngine;
using static Stats;
using static StatFinder;
using UnityEngine.Assertions.Must;

public class CalculationController : MonoBehaviour
{
    private static CalculationController instance;
    public static CalculationController Instance { get { return instance; } }

    private Dictionary<Stat, Stats> playerModifiers;
    private Dictionary<Stat, Stats> skillTreeModifiers;

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
        skillTreeModifiers = new Dictionary<Stat, Stats>();
    }

    public void CalculateSkillStats(string skillTree, ActiveSkillSO skill)
    {
        SetModifiers(skillTree);

        skill.allStats = new List<Stats>(skillTreeModifiers.Values);

        for (int i = 0; i < skill.allStats.Count; i++)
        {
            switch (skill.allStats[i].stat)
            {
                case Stat.SkillDamage:
                    CalculateSkillDamage(skill);
                    break;
                case Stat.ManaCost:
                    PercentageSubtraction(skill, Stat.ManaCost, Stat.ManaCostRecutionP);
                    break;
                case Stat.Cooldown:
                    PercentageSubtraction(skill, Stat.Cooldown, Stat.CooldownReductionP);
                    break;                
                case Stat.CastTime:
                    PercentageSubtraction(skill, Stat.CastTime, Stat.CastSpeedP);
                    break;
            }
        }
    }

    public float DamageOutput(ActiveSkillSO skill)
    {
        // Crit should only be calculated on hit
        return CalculateCrit(skill);
    }

    private void SetModifiers(string skillTree)
    {
        playerModifiers = StatsController.Instance.GetAllPlayerModifiers();

        // Creating a deep copy of the skill modifiers dictionary
        foreach (var kvp in SkillTreeController.instance.GetModifiers(skillTree))
        {
            Stat key = kvp.Key;
            Stats value = new Stats
            {
                stat = kvp.Value.stat,
                minValue = kvp.Value.minValue,
                maxValue = kvp.Value.maxValue
            };
            skillTreeModifiers[key] = value;
        }
    }

    private void PercentageSubtraction(SkillSO skill, Stat mainStat, Stat subStat)
    {
        float value = (1 - FindStat(playerModifiers, subStat).minValue) * FindStat(skillTreeModifiers, mainStat).minValue;

        FindStat(skill, mainStat).minValue = value;
    }

    private void PercentageAddition(SkillSO skill, Stat mainStat, Stat subStat)
    {
        float value = (1 + FindStat(playerModifiers, subStat).minValue) * FindStat(skillTreeModifiers, mainStat).minValue;

        FindStat(skill, mainStat).minValue = value;
    }

    private void Addition(SkillSO skill, Stat mainStat)
    {
        float value = FindStat(skillTreeModifiers, mainStat).minValue + FindStat(playerModifiers, mainStat).minValue;

        FindStat(skill, mainStat).minValue = value;
    }

    private void CalculateSkillDamage(ActiveSkillSO skill)
    {
        // Calculate base damage of the skill
        float skillDamage = FindStat(skillTreeModifiers, Stat.SkillDamage).minValue + (FindStat(skillTreeModifiers, Stat.SkillDamage).maxValue / 100) * FindStat(playerModifiers, Stat.BaseDamage).minValue;
        
        // Additional elemental damage
        float totalDamage = skillDamage + CalculateElementalDamage(skill);

        // Apply calculated damage
        FindStat(skill, Stat.SkillDamage).minValue = totalDamage;
        FindStat(skill, Stat.SkillDamage).maxValue = skillTreeModifiers[Stat.SkillDamage].maxValue;
    }

    private float CalculateElementalDamage(ActiveSkillSO skill)
    {
        // Check for the elemental damage stat
        Enum element = skill.damageType;
        string statName = element.ToString() + "DamageP";
        Stat stat;

        if (Enum.TryParse(statName, out stat))
        {
            float elementalDamage = ((FindStat(playerModifiers, stat).minValue + FindStat(skillTreeModifiers, stat).minValue) / 100) * FindStat(skill, Stat.SkillDamage).minValue;
            return elementalDamage;
        }
        else
        {
            return 0;
        }
    }

    private float CalculateCrit(ActiveSkillSO skill)
    {
        // Calculate Crit stats
        Addition(skill, Stat.CritRateP);
        Addition(skill, Stat.CritDamageP);

        float damage = FindStat(skill, Stat.SkillDamage).minValue;
        float critRate = FindStat(skill, Stat.CritRateP).minValue;
        float critDamage = FindStat(skill, Stat.CritDamageP).minValue;
        
        // Check for critical hit
        float random = UnityEngine.Random.value;
        if (random <= critRate / 100)
        {
            damage *= (1 + critDamage / 100);
            Debug.Log("Crit" + critRate);
            return damage;
        }
        else
        {
            return damage;
        }
    }



    //calculating player stats
    public float CalculatePlayerHealth()
    {
        float baseHealth = StatsController.Instance.GetPlayerModifier(Stat.Health).minValue;
        float percentHealth = StatsController.Instance.GetPlayerModifier(Stat.HealthP).minValue;
        baseHealth *= 1 + percentHealth / 100;

        return baseHealth;
    }


    public float CalculatePlayerMana()
    {
        float baseMana = StatsController.Instance.GetPlayerModifier(Stat.Mana).minValue;
        float percentMana = StatsController.Instance.GetPlayerModifier(Stat.ManaP).minValue;
        float calcMana = (1 + percentMana / 100)*baseMana;

        return calcMana;
    }

    public float CalculatePlayerManaRegen()
    {
        float baseManaRegen = StatsController.Instance.GetPlayerModifier(Stat.ManaRegen).minValue;
        float percentManaRegen = StatsController.Instance.GetPlayerModifier(Stat.ManaRegenP).minValue;
        return (1 + percentManaRegen / 100) * baseManaRegen;
    }

    public float CalculatePlayerHealthRegen()
    {
        float baseHealthRegen = StatsController.Instance.GetPlayerModifier(Stat.HealthRegen).minValue;
        float percentHealthRegen = StatsController.Instance.GetPlayerModifier(Stat.HealthRegenP).minValue;
        return (1 + percentHealthRegen / 100) * baseHealthRegen;
    }

    public float CalculatePlayerArmour()
    {
        float baseArmour = StatsController.Instance.GetPlayerModifier(Stat.HealthRegen).minValue;
        float percentArmour = StatsController.Instance.GetPlayerModifier(Stat.HealthRegenP).minValue;
        return (1 + percentArmour / 100) * baseArmour;
    }

    public float CalculateDamageReduction(float armour)
    {
        //scaling is as follows: first 500 armour will provide 20% reduction
        float trackedArmour = armour;
        float accumulatedDamageReduction = 0f;

        if(trackedArmour >= 500)
        {
            trackedArmour -= 500f;
            accumulatedDamageReduction += .2f;
        }
        else
        {
            accumulatedDamageReduction += (trackedArmour / 500f)*0.2f;
        }

        //2000 armour should provide 35% total reduction
        if(trackedArmour >= 1000)
        {
            trackedArmour -= 1500f;
            accumulatedDamageReduction += .15f;
        }
        else
        {
            accumulatedDamageReduction += (trackedArmour / 1500f) * 0.15f;
        }

        //the rest of the armour will scaling very inefficiently with every additional 1000 providing an additional 10%
        if(trackedArmour > 0)
        {
            accumulatedDamageReduction += (trackedArmour / 1000f) * 0.1f;
        }

        //make sure the total damage reduction doesn't go over cap
        float normalizedDamageReduction = Mathf.Clamp(accumulatedDamageReduction, 0f, 70f);

        return normalizedDamageReduction;
    }
        
}