using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill")]
public class SkillSO : ScriptableObject
{

    public enum DamageType
    {
        NORMAL,
        FIRE,
        ICE,
        ELECTRIC,
        NONE
    }

    public DamageType damageType;
    public List<Stats> allStats { get; private set; } = new List<Stats>();  
    public List<Stats> miscStats = new List<Stats>();

    protected virtual void OnValidate()
    {
        allStats.Clear();
        foreach (Stats stat in miscStats)
        {
            allStats.Add(stat);
        }
    }



}
