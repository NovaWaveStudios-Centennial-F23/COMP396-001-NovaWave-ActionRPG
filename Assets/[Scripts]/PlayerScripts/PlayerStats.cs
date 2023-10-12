using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private List<Stats> playerStats = new List<Stats>();

    public Dictionary<Stats.Stat, Stats> playerModifiers;

    void Start()
    {
        InitModifiers();
    }

    private void InitModifiers()
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

    public void UpdateModifiers(Stats.Stat s, Stats v)
    {
        if (playerModifiers.ContainsKey(s))
        {
            playerModifiers[s] += v;
        }
        else
        {
            playerModifiers.Add(s, v);
        }
    }

    public void UpdatePlayerStats()
    {
        foreach (Stats.Stat s in playerModifiers.Keys)
        {
            for (int i = 0; i < playerStats.Count; i++)
            {
                if (playerStats[i].stat == s)
                {
                    playerStats[i] += playerModifiers[s];
                }
                else
                {
                    playerStats.Add(playerModifiers[s]);
                }
            }
        }
    }
}
