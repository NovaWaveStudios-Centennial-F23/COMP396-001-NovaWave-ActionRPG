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

    public enum GearBase
    {
        Basic,
        Warrior,
        Hero
    }

    public enum GearRarity
    {
        Common = 1,
        Rare = 2,
        Legendary = 4
    }

    public Sprite gearIcon;
    public string gearName;
    // public GameObject groundMesh;
    public GearType gearType;
    public GearBase gearBase;
    public GearRarity gearRarity;
    public List<Stats> mainStats = new List<Stats>();
    public List<Stats> randomRolls = new List<Stats>();
    public List<Stats> affixes = new List<Stats>();
    [TextArea(15, 20)]
    public string gearDescription;

}
