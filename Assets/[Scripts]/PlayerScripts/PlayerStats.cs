using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private List<Stats> playerStats = new List<Stats>();

    private Dictionary<Stats.Stat, Stats> playerModifiers;

    void Start()
    {
        InitPlayerModifiers();
    }

    private void InitPlayerModifiers()
    {
        playerModifiers = new Dictionary<Stats.Stat, Stats>();

        foreach (Stats s in playerStats)
        {
            if (!playerModifiers.ContainsKey(s.stat))
            {
                playerModifiers.Add(s.stat, s);
            }
            else
            {
                playerModifiers[s.stat] = s;
            }
        }
    }

    public void UpdatePlayerStats(Stats.Stat s, Stats v)
    {
        if (playerModifiers.ContainsKey(s))
        {
            playerModifiers[s] += v;
        }
        else
        {
            playerStats.Add(v);
            playerModifiers.Add(playerStats.Find(e => e == v).stat, playerStats.Find(e => e == v));
        }
    }

    public Dictionary<Stats.Stat, Stats> GetAllPlayerStats()
    {
        return playerModifiers;
    }

    public Stats GetPlayerStat(Stats.Stat stat)
    {
        Stats st = null;
        foreach (Stats s in playerStats)
        {
            if (s.stat == stat)
            {
                st = s;
            }
        }
        return st;
    }
}
