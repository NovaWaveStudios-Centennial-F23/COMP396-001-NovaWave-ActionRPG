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
        BlockChanceP,
        DamageReflectP,
        CritRateP,
        CritDamageP,
        MovementSpeed,
        CooldownReductionP,
        ManaCostRecutionP,
        CastSpeedP,
        DoubleCastP,
        DropRateP,
        ItemRarityP,
        SpellDamageP,
        FireDamageP,
        FrostDamageP,
        ElectroDamageP,
        ElementalDamageP,
        FireResP,
        FrostResP,
        ElectroResP,
        ElementalResP,
        FireAffinity,
        FrostAffinity,
        ElectroAffinity,
        SkillDamage,
        ManaCost,
        Cooldown,
        AOE,
        CastTIme,
        Duration,
        ProjectileSpeed,
        Range,
        Burning,
        Slowness,
        Stun,
        FireballDoubleCast
    }

    public Stat stat;
    public float minValue;
    public float maxValue;

    public static Stats operator +(Stats stats, Stats other)
    {
        stats.minValue += other.minValue;
        stats.maxValue += other.maxValue;
        return stats;
    }
}

