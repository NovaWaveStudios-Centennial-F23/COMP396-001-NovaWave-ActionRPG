using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationController : MonoBehaviour
{
    private CalculationController instance;
    public CalculationController Instance { get { return instance; } }

    [Header("Controllers")]
    [SerializeField] private SkillsController skillsController;

    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EquippedGear equippedGear;

    [Header("Skill Tress")]
    [SerializeField] private PlayerSkillTree playerSkillTree;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculatePlayerStats()
    {
        List<Stats> gearStats = CalculateGearStats();

        // Calculate Player Stats from the skill tree
        foreach (Stats stat in playerStats.playerStats) 
        {
            foreach (Node node in playerSkillTree.playerNodes)
            {
                for (int i = 0; i < node.nodeStats.Count; i++)
                {
                    if (stat.stat == node.nodeStats[i].stat)
                    {
                        stat.statValue += node.nodeValues[i];
                    }
                }
            }
        }

        // Calculate Player Stats from gear equipped
        foreach (Stats stat in playerStats.playerStats)
        {
            foreach (Stats gearstat in gearStats)
            {
                if (stat.stat == gearstat.stat)
                {
                    stat.statValue += gearstat.statValue;
                }
            }
        }
    }

    public List<Stats> CalculateGearStats()
    {
        List<Stats> gearStats = new List<Stats>();
        List<Gear> gears = new List<Gear>();

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
            foreach (Stats s in g.gearStats)
            {
                foreach (Stats t in gearStats)
                {
                    if (t != s)
                    {
                        gearStats.Add(s);
                    }
                    else if (t == s)
                    {
                        t.statValue += s.statValue;
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
            }
        }       

        return gearStats;
    }

}
