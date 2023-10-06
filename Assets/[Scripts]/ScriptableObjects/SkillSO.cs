using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Skills/Create New Skill")]
public class SkillSO : ScriptableObject
{
    public int level;
    public enum DamageType
    {
        NORMAL,
        FIRE,
        ICE,
        ELECTRIC
    }

    public DamageType damageType;
    public List<Stats> stats = new List<Stats>();
}
