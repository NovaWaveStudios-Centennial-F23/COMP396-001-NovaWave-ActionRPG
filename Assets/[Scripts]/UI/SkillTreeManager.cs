using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public enum SkillTree
    {
        Player,
        Fireball
    }

    //collection of all skillNodes
    private SkillTreeNode[] nodes;

    [SerializeField]
    private Dictionary<Stats.Stat, Stats> skillTreeModifiers;

    [SerializeField]
    private SkillTree skillTree;

    private void Start()
    {
        nodes = GetComponentsInChildren<SkillTreeNode>();
        RecalculateModifiers();
    }

    public void RecalculateModifiers()
    {
        skillTreeModifiers = new Dictionary<Stats.Stat, Stats>();
        
        if (nodes.Length > 0)
        {
            foreach (SkillTreeNode node in nodes)
            {
                if (node.GetCurrentLevel() > 0)//checks if skill has been allocated
                {
                    List<Stats> activeStats = node.GetStats();
                    if (activeStats.Count > 0)
                    {
                        foreach (Stats s in activeStats)
                        {
                            if (skillTreeModifiers.ContainsKey(s.stat))
                            {
                                skillTreeModifiers[s.stat] += s;
                            }
                            else
                            {
                                skillTreeModifiers.Add(s.stat, s);
                            }
                        }
                    }
                }
            }
        }

    }

    public Dictionary<Stats.Stat, Stats> GetStats()
    {
        return skillTreeModifiers;
    }

    public SkillTree GetSkillTree()
    {
        return skillTree;
    }



}

