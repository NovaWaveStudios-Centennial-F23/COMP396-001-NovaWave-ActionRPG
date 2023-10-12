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
    public Dictionary<Stats.Stat, Stats> modifiers;
    public SkillTree skillTree;

    private void Start()
    {
        nodes = GetComponentsInChildren<SkillTreeNode>();
        RecalculateModifiers();
    }

    public void RecalculateModifiers()
    {
        modifiers = new Dictionary<Stats.Stat, Stats>();
        
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
                            if (modifiers.ContainsKey(s.stat))
                            {
                                modifiers[s.stat] += s;
                            }
                            else
                            {
                                modifiers.Add(s.stat, s);
                            }
                        }
                    }
                }
            }
        }

    }


    public Dictionary<Stats.Stat, float> GetStats(Stats.Stat stat)
    {
        return modifiers;
    }
}

