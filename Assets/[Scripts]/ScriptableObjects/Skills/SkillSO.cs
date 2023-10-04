using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float minDamage;
    public float maxDamage;
    public float castTime;
    public float cooldown;
    public float duration;
    public float radius;
}
