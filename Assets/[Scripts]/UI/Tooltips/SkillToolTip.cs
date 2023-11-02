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


    private List<TextMeshProUGUI> optionalTooltipElements;

    private bool isActiveSkill = false;

    private void Awake()
    {
        optionalTooltipElements = new List<TextMeshProUGUI>()
        {
            txtManaCost,
            txtCdr,
            txtNextSkillDescription,
        };
    }

    public override void DisplayDetails(SkillTreeNode node)
    {

        isActiveSkill = node.isActiveSkill;

        DisplayIcon(node.nodeData.icon);
        DisplayName(node.nodeData.skillName);
        DisplayLevel(node.GetCurrentLevel(), node.maxLevel);

        //turn off all optional elements
        foreach(var t in optionalTooltipElements)
        {
            t.gameObject.SetActive(false);
        }

        
        if (isActiveSkill)
        {
            ActiveSkillSO skillInfo;

            //if skill is not allocated
            if (node.GetCurrentSkillSO() == null)
            {
                skillInfo = node.GetNextLevelSO() as ActiveSkillSO;

            }
            else
            {
                skillInfo = node.GetCurrentSkillSO() as ActiveSkillSO;
            }

            Stats skillDamage = null;
            Stats manaCost = null;
            Stats cooldown = null;

            foreach(Stats s in skillInfo.allStats)
            {
                if(s.stat == Stats.Stat.SkillDamage)
                {
                    skillDamage = s;
                }
                else if(s.stat == Stats.Stat.ManaCost)
                {
                    manaCost = s;

                }else if(s.stat == Stats.Stat.Cooldown)
                {
                    cooldown = s;
                }
            }

            DisplayMana(manaCost);
            DisplayCoolDown(cooldown);
            DisplaySkillDescription(skillDamage);

            if (node.GetCurrentSkillSO() != null && node.GetNextLevelSO() != null)
            {
                DisplayNextLevelInfo(node.GetCurrentSkillSO(), node.GetNextLevelSO());
            }

        }
        else
        {
            PassiveSkillSO skillInfo;
            //if skill is not allocated
            if (node.GetCurrentSkillSO() == null)
            {
                skillInfo = node.GetNextLevelSO() as PassiveSkillSO;

            }
            else
            {
                skillInfo = node.GetCurrentSkillSO() as PassiveSkillSO;
            }

            DisplayModifiers(skillInfo.allStats);

            if (node.GetCurrentSkillSO() != null && node.GetNextLevelSO() != null)
            {
                DisplayNextLevelInfo(node.GetCurrentSkillSO(), node.GetNextLevelSO());
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

    private void DisplayMana(Stats manaCost)
    {
        txtManaCost.text = manaCost.ToString(isActiveSkill);
        txtManaCost.gameObject.SetActive(true);
    }

    private void DisplayCoolDown(Stats cooldown)
    {
        txtCdr.text = cooldown.ToString(isActiveSkill);
        txtCdr.gameObject.SetActive(true);
    }

    private void DisplaySkillDescription(Stats baseDamage)
    {
        string finalDescription = "";

        if(baseDamage != null)
        {
            finalDescription += $"{baseDamage.ToString(isActiveSkill)}";
        }

        txtSkillDescription.text = finalDescription;
    }

    private void DisplayNextLevelInfo(SkillSO currentSkillData, SkillSO nextSkillData)
    {
        string nextLevelInfo = "Next Level:";

        var currentStats = currentSkillData.allStats;
        var nextStats = nextSkillData.allStats;

        List<Stats> changedStats = new List<Stats>(nextStats);

        //check all the stats for the next level and see if its different from the current
        foreach(Stats stat in nextStats)
        {
            //try to find the stat in the currentStats
            foreach(Stats stat2 in currentStats)
            {
                if(stat2 == stat)
                {
                    //if its found, then test to see if the value has changed
                    if(stat2.minValue == stat.minValue || stat2.maxValue == stat.maxValue) 
                    {
                        changedStats.Remove(stat);
                    }
                }
            }
        }

        foreach(Stats s in changedStats)
        {
            nextLevelInfo += $"\n{s.ToString(isActiveSkill)}";
        }



        txtNextSkillDescription.text = nextLevelInfo;

        txtNextSkillDescription.gameObject.SetActive(true);
    }

    private void DisplayModifiers(List<Stats> stats)
    {
        string modifiers = "";
        for(int i = 0; i < stats.Count; i++)
        {
            if(i != 0)
            {
                modifiers += "\n";
            }
            modifiers += stats[i].ToString();
        }

        txtSkillDescription.text = modifiers;
    }

}
