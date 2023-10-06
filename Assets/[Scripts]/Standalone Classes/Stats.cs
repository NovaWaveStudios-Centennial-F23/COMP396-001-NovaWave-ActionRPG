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
        SkillDamage,
        CastTime,
        Cooldown,
        Duration,
        Radius,
        ProjectileSpeed,
        Range,
        Burning,
        DoubleCast,
        ManaCost
    }
    public Stat stat;
    public float value;
    public float deviation;
}

