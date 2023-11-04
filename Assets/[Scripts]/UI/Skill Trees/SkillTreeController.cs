/**Created by Han Bi
 * Used to hold data for stats that come from skill trees
 * Last modified: Oct. 23, 2023
 */
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    public static SkillTreeController instance;

    [Obsolete("this enum will be removed in favour of strings instead, please talk to Charlie for alternatives")]
    public enum SkillTree
    {
        Player,
        Fireball
    }

    //all skilltree parents
    [SerializeField]
    private List<GameObject> skillTrees;

    //collection of all skillNodes
    private List<SkillTreeNode[]> treeNodes = new List<SkillTreeNode[]>();

    [SerializeField]
    private Dictionary<string, Dictionary<Stats.Stat, Stats>> skillTreeModifiers = new Dictionary<string, Dictionary<Stats.Stat, Stats>>();


    [Obsolete("this property will be removed, talk to Charlie for alternatives")]
    private SkillTree skillTree;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        treeNodes.Clear();

        foreach(GameObject s in skillTrees)
        {
            treeNodes.Add(s.GetComponentsInChildren<SkillTreeNode>());
        }
        RecalculateModifiers();
    }

    public void RecalculateModifiers()
    {
        skillTreeModifiers.Clear();

        foreach (SkillTreeNode[] tree in treeNodes)
        {
            if (tree.Length > 0)
            {
                foreach (SkillTreeNode node in tree)
                {
                    if (node.GetCurrentLevel() > 0)//checks if skill has been allocated
                    {
                        List<Stats> activeStats = node.GetStats();

                        //check to see if the skill tree type is already in dictionary, if it is not, then add it
                        if (!skillTreeModifiers.ContainsKey(node.nodeData.skillTreeType))
                        {
                            skillTreeModifiers[node.nodeData.skillTreeType] = new Dictionary<Stats.Stat, Stats>();
                        }

                        //check to see if the skill tree type already has some similar stats
                        foreach (Stats s in activeStats)
                        {
                            if (skillTreeModifiers[node.nodeData.skillTreeType].ContainsKey(s.stat))
                            {
                                skillTreeModifiers[node.nodeData.skillTreeType][s.stat] += s;
                            }
                            else
                            {
                                skillTreeModifiers[node.nodeData.skillTreeType][s.stat] = s;
                            }
                        }
                    }
                }
            }
        }
    }

    [Obsolete("Will be removed, talk to Charlie for more details")]
    public Dictionary<Stats.Stat, Stats> GetStats()
    {
        //deprecated
        return null;
    }

    [Obsolete("This function will be removed, talk to Charlie for alternatives")]
    public SkillTree GetSkillTree()
    {
        return skillTree;
    }

    public Dictionary<Stats.Stat, Stats> GetModifiers(string skillType)
    {
        if (skillTreeModifiers.ContainsKey(skillType))
        {
            return skillTreeModifiers[skillType];
        }
        else
        {
            return new Dictionary<Stats.Stat, Stats>();
        }
    }

    //for testing
    public void Test()
    {
        RecalculateModifiers();
        Dictionary<Stats.Stat, Stats> modifiers = GetModifiers(nameof(Fireball));

        if (modifiers.ContainsKey(Stats.Stat.BaseDamage))
        {
            Debug.Log($"Fireball Base damage (max): {modifiers[Stats.Stat.BaseDamage].maxValue}");
        }
        else
        {
            Debug.Log("Could not find base damage of fireball");
        }

        
    }


}
