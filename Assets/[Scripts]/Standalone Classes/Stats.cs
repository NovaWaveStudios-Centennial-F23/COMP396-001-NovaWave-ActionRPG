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
        CastTime,               // Skill cast time
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
        MovementSpeedP,          // Unit movement speed percentage
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
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue} to base damage";
                break;

            case Stat.Health:
                ans += minValue >= 0 ? "+" : "";//note we don't actually need to put a minus sign because it will already be there
                ans += $"{minValue} to maximum health";
                break;

            case Stat.Mana:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue} to maximum mana";
                break;

            case Stat.Armor:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue} to armour";
                break;

            case Stat.BlockChanceP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to damage reflect percentage";
                break;

            case Stat.DamageReflectP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to reflect percentage";
                break;

            case Stat.CritRateP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to crit rate percentage";
                break;

            case Stat.CritDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to crit damage percentage";
                break;

            case Stat.ManaCost:
                ans += $"Mana: {minValue}";
                break;

            case Stat.MovementSpeed:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue} to movement speed";
                break;

            case Stat.CooldownReductionP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to cooldown reduction percentage";
                break;

            case Stat.ManaCostRecutionP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to mana cost reduction percentage";
                break;

            case Stat.CastSpeedP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to cast speed percentage";
                break;

            case Stat.DoubleCastP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to double drop percentage";
                break;

            case Stat.DropRateP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to drop player rate percentage";
                break;

            case Stat.ItemRarityP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to item rarity";
                break;

            case Stat.SpellDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to spell damage";
                break;

            case Stat.FireDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to fire damage percentage";
                break;

            case Stat.FrostDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to frost damage percentage";
                break;

            case Stat.ElectroDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to electro damage percentage";
                break;

            case Stat.ElementalDamageP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to elemental damage percentage";
                break;

            case Stat.FireResP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to fire resistance percentage";
                break;

            case Stat.FrostResP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to frost resistance percentage";
                break;

            case Stat.ElectroResP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to electro resistance percentage";
                break;

            case Stat.ElementalResP:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}% to elemental resistance percentage";
                break;

            case Stat.FireAffinity:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}+ to fire affinity";
                break;

            case Stat.FrostAffinity:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}+ to frost affinity";
                break;

            case Stat.ElectroAffinity:
                ans += minValue >= 0 ? "+" : "";
                ans += $"{minValue}+ to electro affinity";
                break;           
            
            case Stat.SkillDamage:
                //only shows for skills
                ans += $"Deals {minValue} + {maxValue}% of base attack";
                break;

            case Stat.Cooldown:
                ans += $"Cooldown: {minValue:0.00}s";
                break;

            case Stat.AOE:
                ans += $"Skill area of effect: {minValue}";
                break;

            case Stat.CastTime:
                ans += $"Skill cast time: {minValue}";
                break;

            case Stat.Duration:
                ans += $"Skill duration: {minValue}";
                break;

            case Stat.ProjectileSpeed:
                ans += $"Skill projectile speed: {minValue}";
                break;

            case Stat.Range:
                ans += $"Skill range: {minValue}";
                break;

            case Stat.Burning:
                ans += $"Burning: {minValue}";
                break;

            case Stat.Slowness:
                ans += $"Slowness: {minValue}";
                break;

            case Stat.Stun:
                ans += $"Stun: {minValue}";
                break;

            case Stat.FireballDoubleCast:
                ans += $"Fireball Double Cast: {minValue}";
                break;

            case Stat.HealthP:
                ans += $"{minValue}% of maximum health";
                break;

            case Stat.ManaP:
                ans += $"{minValue}% of maximum mana";
                break;

            case Stat.ArmorP:
                ans += $"{minValue}% of armor";
                break;

            case Stat.MovementSpeedP:
                ans += $"{minValue}% of movement speed";
                break;

            case Stat.ManaRegen:
                ans += $"{minValue} mana regeneration";
                break;

            case Stat.ManaRegenP:
                ans += $"{minValue}% of mana regeneration";
                break;

            case Stat.HealthRegen:
                ans += $"{minValue} health regeneration";
                break;

            case Stat.HealthRegenP:
                ans += $"{minValue}% of health regeneration";
                break;

            case Stat.LifeRecoveryP:
                ans += $"{minValue}% life recovery";
                break;

            case Stat.DamageWWantP:
                ans += $"{minValue}% damage with wand equipped";
                break;

            case Stat.DamageWStaffP:
                ans += $"{minValue}% damage with staff equipped";
                break;

            case Stat.DamageWSheildP:
                ans += $"{minValue}% damage with shield equipped";
                break;

            default: return $"Unimplemented ToString method for ${nameof(stat)}";
        }

        return ans;
    }
}

