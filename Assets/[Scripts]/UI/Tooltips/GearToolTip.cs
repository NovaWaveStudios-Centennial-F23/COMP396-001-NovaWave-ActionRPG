/* Created by: Yusuke Kuroki
 * Used to display details for Gears
 * It's made from SkillToolTip.cs by Han Bi
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearToolTip : MonoBehaviour
{
    [SerializeField]
    Image iconImage;

    [SerializeField]
    TextMeshProUGUI txtGearName;

    [SerializeField]
    TextMeshProUGUI txtGearLevel;

    [SerializeField]
    TextMeshProUGUI txtGearType;

    [SerializeField]
    TextMeshProUGUI txtGearBase;

    [SerializeField]
    TextMeshProUGUI txtRarity;

    [SerializeField]
    TextMeshProUGUI txtStatsDescription;

    [SerializeField]
    TextMeshProUGUI txtGearDescription;

    private List<TextMeshProUGUI> optionalTooltipElements;

    private bool isActiveSkill = false;

    private void Awake()
    {
        optionalTooltipElements = new List<TextMeshProUGUI>()
        {
            txtGearLevel,
            txtGearType,
            txtGearBase,
            txtRarity,
            txtStatsDescription,
            txtGearDescription
        };
    }

    public void DisplayDetails(GearSO gear)
    {
        DisplayIcon(gear.icon);
        DisplayName(gear.data.gearName);
        DisplayGearLevel(gear.level);
        DisplayGearType(gear.gearType);
        DisplayGearBase(gear.gearBase);
        DisplayGearRarity(gear.gearRarity);
        DisplayGearStats(gear.mainStats, gear.randomRolls, gear.affixes);
        DisplayGearDescription(gear.gearDescription);

        // //turn off all optional elements
        // foreach(var t in optionalTooltipElements)
        // {
        //     t.gameObject.SetActive(false);
        // }
    }

    private void DisplayIcon(Sprite image)
    {
        iconImage.sprite = image;
    }

    private void DisplayName(string name)
    {
        txtGearName.text = name;
    }

    private void DisplayGearLevel(int level)
    {
        txtGearLevel.text = $"Level: {level}";
    }


    private void DisplayGearType(GearSO.GearType type)
    {
        txtGearType.text = type.ToString();
    }

    private void DisplayGearBase(GearSO.GearBase gearBase)
    {
        txtGearBase.text = gearBase.ToString();
    }

    private void DisplayGearRarity(GearSO.GearRarity rarity)
    {
        txtRarity.text = rarity.ToString();
    }

    private void DisplayGearStats(List<Stats> mainStats, List<Stats> randomRolls, List<Stats> affixes)
    {
        string statsDescription = "";

        foreach(Stats stat in mainStats)
        {
            statsDescription += $"{stat.ToString(isActiveSkill)}\n";
        }

        foreach(Stats stat in randomRolls)
        {
            statsDescription += $"{stat.ToString(isActiveSkill)}\n";
        }

        foreach(Stats stat in affixes)
        {
            statsDescription += $"{stat.ToString(isActiveSkill)}\n";
        }

        txtStatsDescription.text = statsDescription;
    }

    private void DisplayGearDescription(string description)
    {
        txtGearDescription.text = description;
    }
}
