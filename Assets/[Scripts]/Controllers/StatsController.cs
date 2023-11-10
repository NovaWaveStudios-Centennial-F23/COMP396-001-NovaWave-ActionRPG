/* Created by Sukhmannat Singh
 * Used to set stats for player and skills
 * Last modified Oct 25, 2023
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Stats;
using static StatFinder;

public class StatsController : MonoBehaviour
{
    private static StatsController instance;
    public static StatsController Instance { get { return instance; } }

    [SerializeField] private List<Stats> playerStats;
    [SerializeField] private EquippedGear equippedGear;

    private Dictionary<Stat, Stats> playerModifiers;
    private Dictionary<Stat, Stats> skillTreeModifiers;
    private Dictionary<Stat, Stats> gearStats;

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

    private void Start()
    {
        skillTreeModifiers = new Dictionary<Stat, Stats>();
        gearStats = new Dictionary<Stat, Stats>();
        InitPlayerModifiers();
        playerStats = GetAllPlayerStats();
        CalculatePlayerStats();
    }

    private void InitPlayerModifiers()
    {
        playerModifiers = new Dictionary<Stat, Stats>();

        foreach (Stats s in playerStats)
        {
            playerModifiers.Add(s.stat, s);
        }
    }

    private void CalculatePlayerStats()
    {
        //skillTreeModifiers = SkillTreeController.instance.GetModifiers("Player");
        CalculateGearStats();

        // Duplicate dict to traverse through it
        Dictionary<Stat, Stats> dict = new Dictionary<Stat, Stats>(GetAllPlayerModifiers());

        // Change player stats based on skill tree stats and gear stats
        foreach (Stat s in dict.Keys)
        {
            bool hasGearStat = gearStats.ContainsKey(s);
            bool hasSkillTreeStat = skillTreeModifiers.ContainsKey(s);

            if (hasGearStat && hasSkillTreeStat)
            {
                Stats result = playerStats.Find(x => x.stat == s) + gearStats[s] + skillTreeModifiers[s];
                SetPlayerModifier(s, result);
            }
            else if (!hasGearStat && hasSkillTreeStat)
            {
                Stats result = playerStats.Find(x => x.stat == s) + skillTreeModifiers[s];
                SetPlayerModifier(s, result);

            }
            else if (hasGearStat && !hasSkillTreeStat)
            {
                Stats result = playerStats.Find(x => x.stat == s) + gearStats[s];
                SetPlayerModifier(s, result);
            }
        }

        Debug.Log(GetAllPlayerModifiers()[0].minValue);
        Debug.Log(GetAllPlayerModifiers()[0].maxValue);
        Debug.Log(skillTreeModifiers.Count);
    }

    private void CalculateGearStats()
    {
        this.gearStats.Clear();
        List<Gear> gears = new List<Gear>();
        Dictionary<Stat, Stats> gearStats = new Dictionary<Stat, Stats>();

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
            foreach (Stat s in g.GetGearStats().Keys)
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

    public Dictionary<Stat, Stats> GetAllPlayerModifiers()
    {
        return playerModifiers;
    }

    public List<Stats> GetAllPlayerStats()
    {
        return playerStats;
    }

    public Stats GetPlayerModifier(Stat stat)
    {
        Stats st = null;
        foreach (Stat s in playerModifiers.Keys)
        {
            if (s == stat)
            {
                st = playerModifiers[s];
            }
        }
        return st;
    }

    public void SetPlayerModifier(Stat stat, Stats value)
    {
        foreach (Stats s in playerStats)
        {
            if (s.stat == stat)
            {
                playerModifiers[s.stat] = value;
            }
            else
            {
                Debug.Log(s.stat + "doesn't exist in player stats");
            }
        }
    }
}
