/* Creted by Sukhmannat Singh & Han Bi
 * Contains the enum for all stats and operator overrides
 * Last updated Oct 26, 2023
 */

using System;
using Unity.VisualScripting;

[System.Serializable]
public class Stats
{
    public enum Stat
    {
        BaseDamage,             // Unit Base Damage (applies to all damage)
        Health,                 // Unit Health
        Mana,                   // Unit Mana
        Armor,                  // Unit Armor (blocks some amount of damage inflicted on self)
        BlockChanceP,           // Unit block chance percentage (chance to completely block an attack)
        DamageReflectP,         // Unit damage reflect percentage (reflect a percentage of damage taken back to attacker)
        CritRateP,              // Unit crit rate percentage (chance to land critical hits)
        CritDamageP,            // Unit crit damage percentage (the percentage of extra damage dealt on critical hits)
        MovementSpeed,          // Unit movement speed
        CooldownReductionP,     // Unit cooldown reduction percentage (reduces the amount of cooldown on all spells)
        ManaCostRecutionP,      // Player mana cost reduction percentage (reduces the mana cost of all spells)
        CastSpeedP,             // Unit cast speed percentage (increases the cast speed for all spells)
        DoubleCastP,            // Unit double cast percentage (chance to cast a spell twice)
        DropRateP,              // Player drop rate percentage (increases the chance to drop an item)
        ItemRarityP,            // Player item rarity (increases the chance of getting higher tier loot)
        SpellDamageP,           // Unit spell damage (increases the damage of all spells) [Subject to removal as its similar to base damage]
        FireDamageP,            // Unit fire damage percentage (increases the damage of all fire spells)
        FrostDamageP,           // Unit frost damage percentage (increases the damage of all frost spells)
        ElectroDamageP,         // Unit electro damage percentage (increases the damage of all electro spells)
        ElementalDamageP,       // Unit elemental damage percentage (increases the damage of all elemntal spells)
        FireResP,               // Unit fire resistance percentage (decreases the amount of fire damage inflicted on self)
        FrostResP,              // Unit frost resistance percentage (decreases the amount of frost damage inflicted on self)
        ElectroResP,            // Unit electro resistance percentage (decreases the amount of electro damage inflicted on self)
        ElementalResP,          // Unit elemental resistance percentage (decreases the amount of all elemental damage inflicted on self)
        FireAffinity,           // Unit fire affinity (increases the burning damage and burn duration)
        FrostAffinity,          // Unit frost affinity (increases the slow percentage and slow duration)
        ElectroAffinity,        // Unit electro affinity (increases the stun duration)
        SkillDamage,            // Skill base damage (applies to a specific skill)
        ManaCost,               // Skill mana cost 
        Cooldown,               // Skill cooldown 
        AOE,                    // Skill area of effect
        CastTIme,               // Skill cast time
        Duration,               // Skill duration
        ProjectileSpeed,        // Skill projectile speed
        Range,                  // Skill range
        Burning,                // [Subject to removal] (More of a state than a stat)
        Slowness,               // [Subject to removal] (More of a state than a stat)
        Stun,                   // [Subject to removal] (More of a state than a stat)
        FireballDoubleCast,     // [Subject to removal] (Is a skill tree/effect node, not particularly a stat)
        HealthP,                // Unit health percentage
        ManaP,                  // Unit mana percentage
        ArmorP,                 // Unit mrmour percentage
        MovmentSpeedP,          // Unit movement speed percentage
        ManaRegen,              // Unit mana regeneration
        ManaRegenP,             // Unit mana regeneration percentage
        HealthRegen,            // Unit health regenration
        HealthRegenP,           // Unit health regeneration percentage
        LifeRecoveryP,          // Unit amount of health recovered percentage
        DamageWWantP,           // Player damage with wand equipped percentage
        DamageWStaffP,          // Player damage with staff equpped percentage
        DamageWSheildP,         // Player damage with shield equipped percentage
    }

    public Stat stat;
    public float minValue;
    public float maxValue;

    public static Stats operator +(Stats stats, Stats other)
    {
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
                ans += maxValue >= 0 ? "+" : "";
                ans += $"{maxValue} to base damage";
                break;
            case Stat.Health:
                ans += maxValue >= 0 ? "+" : "";//note we don't actually need to put a minus sign because it will already be there
                ans += $"{maxValue} to maximum health";
                break;
            case Stat.Mana:
                ans += maxValue >= 0 ? "+" : "";
                ans += $"{maxValue} to maximum mana";
                break;
            case Stat.Armor:
                ans += maxValue >= 0 ? "+" : "";
                ans += $"{maxValue} to armour";
                break;
            case Stat.ManaCost:
                ans += $"Mana: {maxValue}";
                break;
            case Stat.Cooldown:
                ans += $"Cooldown: {maxValue:0.00}s";
                break;
            case Stat.SkillDamage:
                //only shows for skills
                ans += $"Deals {minValue} + {maxValue}% of base attack";
                break;
            default: return $"Unimplemented ToString method for ${nameof(stat)}";
        }

        return ans;
    }
}

