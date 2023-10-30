/* Created by Sukhmannat Singh
 * Used to set stats for player and skills
 * Last modified Oct 25, 2023
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private static StatsController instance;
    public static StatsController Instance { get { return instance; } }

    [Header("Controllers")]
    [SerializeField] private SkillsController skillsController;

    [Header("Player Stats")]
    [SerializeField] private List<Stats> playerStats;
    [SerializeField] private EquippedGear equippedGear;

    private Dictionary<Stats.Stat, Stats> playerModifiers;
    private Dictionary<Stats.Stat, Stats> skillTreeStats;
    private Dictionary<Stats.Stat, Stats> gearStats;
    private List<Stats> initialPlayerStats;

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

    private void Start()
    {
        skillTreeStats = new Dictionary<Stats.Stat, Stats>();
        gearStats = new Dictionary<Stats.Stat, Stats>();
        InitPlayerModifiers();
        initialPlayerStats = GetAllPlayerStats();
    }

    void Update()
    {
        // Manual Test to calculate player stats
        if (Input.GetKeyDown(KeyCode.L))
        {
            CalculatePlayerStats();
            Debug.Log(GetAllPlayerModifiers()[0].minValue);
            Debug.Log(GetAllPlayerModifiers()[0].maxValue);
        }
    }

    private void InitPlayerModifiers()
    {
        playerModifiers = new Dictionary<Stats.Stat, Stats>();

        foreach (Stats s in playerStats)
        {
            playerModifiers.Add(s.stat, s);
        }
    }

    public void SetSkillTreeStats(string skillTree)
    {
        // Get skill tree modifiers from Skill Tree Controller
        skillTreeStats = SkillTreeController.instance.GetModifiers(skillTree);
    }

    private void CalculatePlayerStats()
    {
        SetSkillTreeStats("Fireball");
        CalculateGearStats();

        // Duplicate dict to traverse through it
        Dictionary<Stats.Stat, Stats> dict = new Dictionary<Stats.Stat, Stats>(GetAllPlayerModifiers());

        // Change player stats based on skill tree stats and gear stats
        foreach (Stats.Stat s in dict.Keys)
        {
            bool hasGearStat = gearStats.ContainsKey(s);
            bool hasSkillTreeStat = skillTreeStats.ContainsKey(s);

            if (hasGearStat && hasSkillTreeStat)
            {
                Stats result = initialPlayerStats[initialPlayerStats.IndexOf(dict[s])] + gearStats[s] + skillTreeStats[s];
                SetPlayerModifier(s, result);
            }
            else if (!hasGearStat && hasSkillTreeStat)
            {
                Stats result = initialPlayerStats[initialPlayerStats.IndexOf(dict[s])] + skillTreeStats[s];
                SetPlayerModifier(s, result);

            }
            else if (hasGearStat && !hasSkillTreeStat)
            {
                Stats result = initialPlayerStats[initialPlayerStats.IndexOf(dict[s])] + gearStats[s];
                SetPlayerModifier(s, result);
            }
        }
    }
    
    private void CalculateGearStats()
    {
        this.gearStats.Clear();
        List<Gear> gears = new List<Gear>();
        Dictionary<Stats.Stat, Stats> gearStats = new Dictionary<Stats.Stat, Stats>();

        // List of equipped gear (Cannot be a list)
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
            foreach (Stats.Stat s in g.GetGearStats().Keys)
            {
                if (gearStats.ContainsKey(s))
                {
                    gearStats[s] += g.GetGearStats()[s];
                    break;
                }
                else
                {
                    gearStats.Add(s, g.GetGearStats()[s]);
                    break;
                }
            }
        }

        this.gearStats = gearStats;
    }

    public Dictionary<Stats.Stat, Stats> GetAllPlayerModifiers()
    {
        return playerModifiers;
    }

    public List<Stats> GetAllPlayerStats()
    {
        return playerStats;
    }

    public Stats GetPlayerModifier(Stats.Stat stat)
    {
        Stats st = null;
        foreach (Stats.Stat s in playerModifiers.Keys)
        {
            if (s == stat)
            {
                st = playerModifiers[s];
            }
        }
        return st;
    }

    public void SetPlayerModifier(Stats.Stat stat, Stats value)
    {
        foreach (Stats s in playerStats)
        {
            if (s.stat == stat)
            {
                playerModifiers[s.stat] = value;
            }
            else
            {
                Debug.Log("Stat doesn't exist in player stats");
            }
        }
    }
}
