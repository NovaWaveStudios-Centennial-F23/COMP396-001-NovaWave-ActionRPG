using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public static class StatFinder
{
    public static Stats FindStat(SkillSO skill, Stat stat)
    {
        Stats stats = skill.allStats.Find(x => x.stat == stat);

        if (stats == null)
        {
            stats = new Stats
            {
                stat = stat,
                minValue = 0,
                maxValue = 0
            };
        }

        return stats;
    }

    public static Stats FindStat(Dictionary<Stat, Stats> modifiers, Stat stat)
    {
        Stats stats = modifiers[stat];

        if (stats == null)
        {
            stats = new Stats
            {
                stat = stat,
                minValue = 0,
                maxValue = 0
            };
        }

        return stats;
    }
}
