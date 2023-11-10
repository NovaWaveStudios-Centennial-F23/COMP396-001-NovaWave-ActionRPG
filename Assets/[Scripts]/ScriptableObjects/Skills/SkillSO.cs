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
        Normal,
        Fire,
        Frost,
        Electro,
        None
    }

    public enum SkillType
    {
        Projectile,
        OnPlayer,
        OnMouse
    }

    public GameObject prefab;
    public DamageType damageType;
    public SkillType skillType;
    public List<Stats> allStats { get; set; } = new List<Stats>();
    public List<Stats> miscStats = new List<Stats>();

    protected virtual void OnValidate()
    {
        allStats.Clear();
        foreach (Stats stat in miscStats)
        {
            allStats.Add(stat);
        }
    }

    protected virtual void Awake()
    {
        allStats.Clear();
        foreach (Stats stat in miscStats)
        {
            allStats.Add(stat);
        }
    }
}
