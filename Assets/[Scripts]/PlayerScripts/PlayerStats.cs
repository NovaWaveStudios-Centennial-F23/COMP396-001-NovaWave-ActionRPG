using System;
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
            playerModifiers.Add(s.stat, s);
        }
    }

    public Dictionary<Stats.Stat, Stats> GetAllPlayerModifiers()
    {
        return playerModifiers;
    }

    public List<Stats> GetAllPlayerStats()
    {
        return playerStats;
    }

    public Stats GetPlayerModifier(Stats.Stat stat)
    {
        Stats st = null;
        foreach (Stats.Stat s in playerModifiers.Keys)
        {
            if (s == stat)
            {
                st = playerModifiers[s];    
            }
        }
        return st;
    }

    public void SetPlayerModifier(Stats.Stat stat, Stats value)
    {
        foreach (Stats s in playerStats)
        {
            if (s.stat == stat)
            {
                playerModifiers[s.stat] = value;
            }
            else
            {
                Debug.Log("Stat doesn't exist in player stats");
            }
        }
    }
}
