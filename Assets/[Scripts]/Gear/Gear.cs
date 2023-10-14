using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    private List<Stats> gearStats;

    private Dictionary<Stats.Stat, Stats> gearModifiers;

    private void Awake()
    {
        gearModifiers = new Dictionary<Stats.Stat, Stats>();
        foreach (Stats s in gearStats)
        {
            gearModifiers.Add(s.stat, s);
        }
    }

    public void InitGearStats()
    {
        foreach (Stats.Stat s in gearModifiers.Keys)
        {
            gearStats.Add(gearModifiers[s]);
        }
    }

    public Dictionary<Stats.Stat, Stats> GetGearStats() 
    {
        return gearModifiers;
    }
}
