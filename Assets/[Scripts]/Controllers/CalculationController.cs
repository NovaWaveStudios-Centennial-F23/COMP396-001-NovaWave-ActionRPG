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
    [SerializeField] private SkillTreeManager fireballSkillTree;

    private Dictionary<Stats.Stat, Stats> playerSkillTreeStats;
    private Dictionary<Stats.Stat, Stats> gearStats;
    private Dictionary<Stats.Stat, Stats> fireballStats;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CalculateGearStats();
        }
    }

    public void SkillTreeCalculation(SkillTreeManager.SkillTree skillTree)
    {
        switch (skillTree)
        {
            case SkillTreeManager.SkillTree.Player:
                SetPlayerSkillTreeStats();
                break;
            case SkillTreeManager.SkillTree.Fireball:
                CalculateFireballStats();
                break;
            default:
                break;

        }
    }

    private void CalculatePlayerStats()
    {
        foreach (Stats.Stat s in playerStats.GetAllPlayerStats().Keys)
        {
            foreach (Stats.Stat t in playerSkillTreeStats.Keys)
            {
                if (s == t)
                {
                    playerStats.GetAllPlayerStats()[s] += playerSkillTreeStats[s];
                }
                else
                {
                    playerStats.GetAllPlayerStats().Add(t, playerSkillTreeStats[t]);
                }
            }

            foreach (Stats.Stat u in gearStats.Keys)
            {
                if (s == u)
                {
                    playerStats.GetAllPlayerStats()[s] += gearStats[s];
                }
                else
                {
                    playerStats.GetAllPlayerStats().Add(u, gearStats[u]);
                }
            }
        }
    }
    
    public void SetPlayerSkillTreeStats()
    {
        playerSkillTreeStats = playerSkillTree.GetStats();
        CalculatePlayerStats();
    }

    private void CalculateGearStats()
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
        CalculatePlayerStats();
    }

    public void CalculateFireballStats()
    {
        fireballStats = fireballSkillTree.GetStats();

        foreach (Stats.Stat s in fireballStats.Keys)
        {
            if (playerStats.GetAllPlayerStats().ContainsKey(s))
            {
                fireballStats[s] += playerStats.GetAllPlayerStats()[s];
            }
        }
    }
}
