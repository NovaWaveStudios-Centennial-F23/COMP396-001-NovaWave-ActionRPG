using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearSciptableObject", menuName = "ScriptableObejcts/Create New Gear")]
public class GearSO : ScriptableObject
{
    public MeshFilter gearMeshFilter;
    public int level;
    public enum GearType
    {
        Wand,
        Staff,
        Shield,
        Helmet,
        Chestplate,
        Gloves,
        Boots
    }

    public enum GearRating
    {
        Low,
        Medium,
        High
    }

    public enum GearRarity
    {
        Common,
        Rare,
        Legendary
    }

    public GearType gearType;
    public GearRating gearRating;
    public GearRarity gearRarity;
    public List<Stats> mainStats = new List<Stats>();
    public List<Stats> randomRolls = new List<Stats>();
    public List<Stats> affixes = new List<Stats>();
}
