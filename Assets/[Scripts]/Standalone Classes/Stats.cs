using System;

[System.Serializable]
public class Stats
{
    public enum Stat
    {
        BaseDamage,
        Health,
        Mana,
        Armor,
        FireRes,
        IceRes,
        ElectricRes,
        FireAffinity,
        IceAffinity,
        ElectricAffinity,
        CooldownReduction,
        CritRate,
        CritDamage,
        SpellDamage,
        FireDamage,
        IceDamage,
        ElectricDamage,
        MinSkillDamage,
        MaxSkillDamage,
        CastTime,
        Cooldown,
        Duration,
        Radius,
        ProjectileSpeed,
        Range,
        Burning,
        DoubleCast
    }
    public Stat stat;
    public float value;
}

