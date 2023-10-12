using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculationController : MonoBehaviour
{
    private static CalculationController instance;
    public static CalculationController Instance { get { return instance; } }

    [Header("Controllers")]
    [SerializeField] private SkillsController skillsController;

    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EquippedGear equippedGear;

    [Header("Skill Tress")]
    [SerializeField] private SkillTreeManager playerSkillTree;
    [SerializeField] private FireballSkillTree fireballSkillTree;

    [Header("Skill Stats")]
    [SerializeField] private List<Stats> fireballStats;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void CalculatePlayerStats()
    {
        Dictionary<Stats.Stat, Stats> gearStats = CalculateGearStats();

        // Calculate Player Stats from the skill tree
        foreach (Stats.Stat s in playerSkillTree.GetStats().Keys)
        {
            playerStats.UpdatePlayerModifiers(s, playerSkillTree.GetStats()[s]);
        }

        // Calculate Player Stats from gear equipped
        foreach (Stats.Stat s in gearStats.Keys)
        {
            playerStats.UpdatePlayerModifiers(s, gearStats[s]);
        }

        playerStats.UpdatePlayerStats();
    }

    private Dictionary<Stats.Stat, Stats> CalculateGearStats()
    {
        List<Gear> gears = new List<Gear>();
        Dictionary<Stats.Stat, Stats> gearStats = new Dictionary<Stats.Stat, Stats>();

        // List of equipped gear
        if (equippedGear.wand != null)
        {
            gears.Add(equippedGear.wand);
        }
        if (equippedGear.staff != null)
        {
            gears.Add(equippedGear.staff);
        }
        if (equippedGear.shield != null)
        {
            gears.Add(equippedGear.shield);
        }
        if (equippedGear.helmet != null)
        {
            gears.Add(equippedGear.helmet);
        }
        if (equippedGear.chestplate != null)
        {
            gears.Add(equippedGear.chestplate);
        }
        if (equippedGear.gloves != null)
        {
            gears.Add(equippedGear.gloves);
        }
        if (equippedGear.boots != null)
        {
            gears.Add(equippedGear.boots);
        }

        //Calculate all stats from gear
        foreach (Gear g in gears)
        {
            foreach (Stats.Stat s in g.gearModifiers.Keys)
            {
                if (gearStats.ContainsKey(s))
                {
                    gearStats[s] += g.gearModifiers[s];
                }
                else
                {
                    gearStats.Add(s, g.gearModifiers[s]);
                }
            }
        }

        return gearStats;
    }

    public void SkillTreeCalculation(SkillTreeManager.SkillTree skillTree)
    {
        switch (skillTree)
        {
            case SkillTreeManager.SkillTree.Player:
                CalculatePlayerStats();
                break;
            default:
                break;

        }
    }
}
