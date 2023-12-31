
using System.Collections.Generic;
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
        Stats stats;

        if (modifiers.ContainsKey(stat))
        {
            stats = modifiers[stat];
        }
        else
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

    public static Stats FindStat(List<Stats> statList, Stat stat)
    {
        Stats stats = statList.Find(x => x.stat == stat);

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
