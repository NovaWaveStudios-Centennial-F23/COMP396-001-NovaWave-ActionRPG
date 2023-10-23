/* Created by: Han Bi
 * Used to display details for skills
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillToolTip : ToolTipHandler
{
    [SerializeField]
    Image iconImage;

    [SerializeField]
    TextMeshProUGUI txtSkillName;

    [SerializeField]
    TextMeshProUGUI txtSkillLevel;

    [SerializeField]
    TextMeshProUGUI txtManaCost;

    [SerializeField]
    TextMeshProUGUI txtCdr;

    [SerializeField]
    TextMeshProUGUI txtSkillDescription;

    [SerializeField]
    TextMeshProUGUI txtNextSkillDescription;


    private List<TextMeshProUGUI> optionaltooltipElements;

    private void Awake()
    {
        optionaltooltipElements = new List<TextMeshProUGUI>()
        {
            txtManaCost,
            txtCdr,
            txtNextSkillDescription,
        };
    }

    public override void DisplayDetails(SkillTreeNode node)
    {
        DisplayIcon(node.nodeData.icon);
        DisplayName(node.nodeData.skillName);
        DisplayLevel(node.GetCurrentLevel(), node.maxLevel);

        //turn off all optional elements
        foreach(var t in optionaltooltipElements)
        {
            t.gameObject.SetActive(false);
        }

        
        if (node.nodeData.nodeType == SkillTreeNodeSO.NodeType.Skill)
        {
            SkillSO skillInfo;

            //if skill is not allocated
            if (node.GetCurrentSkillSO() == null)
            {
                skillInfo = node.GetNextLevelSO();

            }
            else
            {
                skillInfo = node.GetCurrentSkillSO();
            }

            DisplayMana(skillInfo.manaCost.maxValue);
            DisplayCoolDown(skillInfo.cooldown.maxValue);
            DisplaySkillDescription(node.nodeData.description, skillInfo);

            if (node.GetCurrentSkillSO() != null && node.GetNextLevelSO() != null)
            {
                DisplayNextLevelInfo(node.nodeData.description, node.GetCurrentSkillSO(), node.GetNextLevelSO());
            }
            
        }

        
    }

    private void DisplayIcon(Sprite image)
    {
        iconImage.sprite = image;
    }

    private void DisplayName(string name)
    {
        txtSkillName.text = name;
    }

    private void DisplayLevel(int currentLevel, int maxLevel)
    {
        txtSkillLevel.text = $"Level: {currentLevel}/{maxLevel}";
    }

    private void DisplayMana(float manaCost)
    {
        int cost = Mathf.RoundToInt(manaCost);
        txtManaCost.text = $"Mana: {cost}";
        txtManaCost.gameObject.SetActive(true);
    }

    private void DisplayCoolDown(float cooldown)
    {
        string cdr = cooldown.ToString("0.00");
        txtCdr.text = $"Cooldown: {cdr}s";
        txtCdr.gameObject.SetActive(true);
    }

    private void DisplaySkillDescription(string description, SkillSO data)
    {
        string finalDescription = description;

        //find the BaseDamage stat
        Stats baseDamage = null;

        for(int i = 0; i < data.stats.Count; i++) 
        {
            if (data.stats[i].stat == Stats.Stat.BaseDamage)
            {
                baseDamage = data.stats[i];
                break;
            }
        }

        if(baseDamage != null)
        {
            finalDescription += $" {baseDamage.minValue} to {baseDamage.maxValue}% of your attack";
        }

        txtSkillDescription.text = finalDescription;
    }

    //In
    //used to display the changes in a modular way:
    //
    private void DisplayNextLevelInfo(string descripton, SkillSO currSkillData, SkillSO nextSkillData)
    {
        string nextLevelInfo = "Next Level:\n";
        nextLevelInfo += descripton;

        //find the BaseDamage stat
        Stats baseDamage = null;

        for (int i = 0; i < nextSkillData.stats.Count; i++)
        {
            if (nextSkillData.stats[i].stat == Stats.Stat.BaseDamage)
            {
                baseDamage = nextSkillData.stats[i];
                break;
            }
        }

        if (baseDamage != null)
        {
            nextLevelInfo += $" {baseDamage.minValue} to {baseDamage.maxValue}% of your attack";

        }

        txtNextSkillDescription.text = nextLevelInfo;

        txtNextSkillDescription.gameObject.SetActive(true);
    }

}
