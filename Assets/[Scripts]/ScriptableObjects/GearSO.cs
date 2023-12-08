using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearSciptableObject", menuName = "ScriptableObejcts/Create New Gear"), System.Serializable]
public class GearSO : ScriptableObject
{
    //public MeshFilter gearMeshFilter;
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

    public GearType gearType;
    public GearBase gearBase;
    public GearRarity gearRarity;
    public List<Stats> mainStats = new List<Stats>();
    public List<Stats> randomRolls = new List<Stats>();
    public List<Stats> affixes = new List<Stats>();

    // information for inventory
    public Sprite icon;
    [TextArea(15, 20)]
    public string gearDescription;
    public GearInfo data = new GearInfo();

    public GearInfo CreateGear()
    {
        GearInfo newGear = new GearInfo(this);
        return newGear;
    }

    // [System.NonSerialized]
    // public bool stackable = false;
}

// This is for inventory
[System.Serializable]
public class GearInfo
{
    public string gearName;
    public int Id = -1;

    // Constructer
    public GearInfo()
    {
        gearName = "";
        Id = -1;
    }

    public GearInfo(GearSO gear)
    {
        gearName = gear.name;
        Id = gear.data.Id;
    }
}