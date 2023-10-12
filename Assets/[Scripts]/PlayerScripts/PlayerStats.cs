using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private List<Stats> playerStats = new List<Stats>();

    public Dictionary<Stats.Stat, Stats> playerStats;
}
