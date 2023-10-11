using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{


    //collection of all skillNodes
    private SkillTreeNode[] nodes;

    [SerializeField]
    Dictionary<Stats.Stat, float> modifiers;
    private void Start()
    {
        nodes = GetComponentsInChildren<SkillTreeNode>();
        RecalculateModifiers();
    }

    public void RecalculateModifiers()
    {
        modifiers = new Dictionary<Stats.Stat, float>();
        
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
                                modifiers[s.stat] += s.statValue;
                            }
                            else
                            {
                                modifiers.Add(s.stat, s.statValue);
                            }
                        }
                    }
                }
            }
        }

        //for testing
        Debug.Log($"Fire Affinity: {GetStat(Stats.Stat.FireAffinity)}");
    }


    public float GetStat(Stats.Stat stat)
    {
        modifiers.TryGetValue(stat, out var modifer);

        return modifer;
    }
}

