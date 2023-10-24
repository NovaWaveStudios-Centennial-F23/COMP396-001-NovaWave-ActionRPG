using System;
using Unity.VisualScripting;

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
        //I don't think we should be changing the existing class since then we lose the 'record of truth'
        //stats.minValue += other.minValue;
        //stats.maxValue += other.maxValue;

        //check if the stats are the same before adding
        if (!stats.Equals(other))
        {
            throw new ArgumentException("Cannot add two stats of different types together");
        }

        //create new stat with same stat type
        Stats ans = new Stats(stats.stat)
        {
            minValue = stats.minValue + other.minValue,
            maxValue = stats.maxValue + other.maxValue
        };

        return ans;

    }

    public override bool Equals(object obj) => this.Equals(obj as Stats);

    public bool Equals(Stats other)
    {
        if(other == null) return false;

        if(ReferenceEquals(this, other)) return true;

        if(GetType() != other.GetType())
        {
            return false;
        }

        return stat == other.stat;
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

    public string ToString(bool isActiveSkill=false)
    {
        string ans = "";

        switch (stat)
        {
            case Stat.BaseDamage:
                if (!isActiveSkill)
                {
                    ans += minValue >= 0 ? "+" : "-";
                }
                else
                {
                    ans += "Deals ";
                }
                ans += $"{minValue} to {maxValue}% of your attack";

                break;
            case Stat.Health:
                ans += maxValue >= 0 ? "+" : "-";
                ans += $"{maxValue} to maximum health";
                break;
            case Stat.Mana:
                ans += maxValue >= 0 ? "+" : "-";
                ans += $"{maxValue} to maximum mana";
                break;
            case Stat.Armor:
                ans += maxValue >= 0 ? "+" : "-";
                ans += $"{maxValue} to armour";
                break;
            case Stat.ManaCost:
                ans += $"Mana: {maxValue}";
                break;
            case Stat.Cooldown:
                ans += $"Cooldown: {maxValue:0.00}s";
                break;
            default: return $"Unimplemented ToString method for ${nameof(stat)}";
        }

        return ans;
    }



}

