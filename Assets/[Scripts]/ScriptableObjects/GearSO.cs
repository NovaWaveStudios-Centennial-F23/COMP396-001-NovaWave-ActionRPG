using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearSciptableObject", menuName = "ScriptableObejcts/Gear/Create New Gear")]
public class GearSO : MonoBehaviour
{
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
    public List<Stats> stats = new List<Stats>();
}
