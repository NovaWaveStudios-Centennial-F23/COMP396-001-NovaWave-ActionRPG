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
        Debug.Log(gearStats.Keys.Count);

        // Change player modifiers based on skill tree stats and gear stats
        playerModifiers = AddDictionaries(playerModifiers, skillTreeModifiers);
        playerModifiers = AddDictionaries(playerModifiers, gearStats);
        Debug.Log(playerModifiers[0].minValue);
    }

    private void CalculateGearStats()
    {
        this.gearStats.Clear();
        List<Gear> gears = new List<Gear>();
        Dictionary<Stat, Stats> tempGearStats = new Dictionary<Stat, Stats>();

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

        Debug.Log(gears.Count);
        //Calculate all stats from gear
        foreach (Gear g in gears)
        {
            try
            {
                foreach (Stat s in g.GetGearStats().Keys)
                {
                    if (tempGearStats.ContainsKey(s))
                    {
                        tempGearStats[s] += g.GetGearStats()[s];
                        break;
                    }
                    else
                    {
                        tempGearStats.Add(s, g.GetGearStats()[s]);
                        break;
                    }
                }

            }catch (Exception ex)
            {
                Debug.LogWarning($"{ex}");
            }
            
        }

        this.gearStats = tempGearStats;
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
        Stats st = FindStat(playerModifiers, stat);
        return st;
    }

    /*public void SetPlayerModifier(Stat stat, Stats value)
    {
        foreach (Stats s in playerStats)
        {
            if (s.stat == stat)
            {
                playerModifiers[s.stat] = value;
            }
            else
            {
                Debug.Log(s.stat + " doesn't exist in player stats");
            }
        }
    }*/    
}
