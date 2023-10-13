using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsTest : MonoBehaviour
{
    [SerializeField]
    private List<Stats> enemyStats = new List<Stats>();

    private Dictionary<Stats.Stat, Stats> enemyModifiers;

    void Start()
    {
        InitEnemyModifiers();
    }

    private void InitEnemyModifiers()
    {
        enemyModifiers = new Dictionary<Stats.Stat, Stats>();

        foreach (Stats s in enemyStats)
        {
            if (!enemyModifiers.ContainsKey(s.stat))
            {
                enemyModifiers.Add(s.stat, s);
            }
            else
            {
                enemyModifiers[s.stat] = s;
            }
        }
    }

    public void UpdateEnemyModifiers(Stats.Stat s, Stats v)
    {
        if (enemyModifiers.ContainsKey(s))
        {
            enemyModifiers[s] += v;
        }
        else
        {
            enemyModifiers.Add(s, v);
        }
    }

    public void UpdateEnemyStats()
    {
        foreach (Stats.Stat s in enemyModifiers.Keys)
        {
            for (int i = 0; i < enemyStats.Count; i++)
            {
                if (enemyStats[i].stat == s)
                {
                    enemyStats[i] += enemyModifiers[s];
                }
                else
                {
                    enemyStats.Add(enemyModifiers[s]);
                }
            }
        }
    }

    // GET Methods
    public List<Stats> GetAllEnemyStats()
    {
        return enemyStats;
    }

    public Stats GetEnemyStat(Stats.Stat stat)
    {
        Stats st = null;
        foreach (Stats s in enemyStats)
        {
            if (s.stat == stat)
            {
                st = s;
            }
        }
        return st;
    }
}
