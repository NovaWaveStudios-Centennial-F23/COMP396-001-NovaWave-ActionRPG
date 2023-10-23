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

    public override bool Equals(object obj) => this.Equals(obj as Stats);

    public bool Equals(Stats other)
    {
        if(other == null) return false;

        if(ReferenceEquals(this, other)) return true;

        if(this.GetType() != other.GetType())
        {
            return false;
        }

        return this.stat == other.stat;
    }

    //not sure if this is correct
    public override int GetHashCode() => stat.GetHashCode();

    public static bool operator ==(Stats lhs, Stats rhs)
    {
        if(lhs is null)
        {
            if(rhs is null)
            {
                //both are null
                return true;
            }

            //only left side is null
            return false;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Stats lhs, Stats rhs)
    {
        return !(lhs == rhs);
    }




    public Stats(Stat stat)
    {
        this.stat = stat;
    }

    public Stats() { }
}

