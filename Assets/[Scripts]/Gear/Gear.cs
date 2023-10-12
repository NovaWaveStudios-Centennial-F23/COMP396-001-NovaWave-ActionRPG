using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    private List<Stats> gearStats;

    public Dictionary<Stats.Stat, Stats> gearModifiers;
}
