using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill")]
public class SkillSO : ScriptableObject
{
    public int level;
    public Stats manaCost = new Stats(Stats.Stat.ManaCost);
    public enum DamageType
    {
        NORMAL,
        FIRE,
        ICE,
        ELECTRIC
    }

    public DamageType damageType;
    public List<Stats> stats { get; private set; }
    public List<Stats> otherStats;

    private void OnValidate()
    {
        stats = new List<Stats>
        {
            manaCost,
        };

        foreach (Stats stat in otherStats)
        {
            stats.Add(stat);
        }


    }


}
