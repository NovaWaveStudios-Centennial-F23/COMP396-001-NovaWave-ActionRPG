/**Created by: Han Bi
 * Used to load UI information for active skills only
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveSkillUIData : MonoBehaviour
{
    public static ActiveSkillUIData Instance;

    public Dictionary<string, SkillTreeNodeSO> activeSkills = new();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //get all skillNodes (since they connect UI with skill)
        activeSkills.Clear();
        SkillTreeNodeSO[] resources = Resources.LoadAll("SkillNodes", typeof(SkillTreeNodeSO)).Cast<SkillTreeNodeSO>().ToArray();

        foreach (SkillTreeNodeSO resource in resources) 
        {
            //check to see if node is for an active skill
            if (resource.skills[0].GetType() == typeof(ActiveSkillSO))
            {
                activeSkills.Add(resource.skillTreeType, resource);
            }
        }
    }

    public Sprite GetSprite(string skillType)
    {
        if (activeSkills.ContainsKey(skillType))
        {
            return activeSkills[skillType].icon;
        }
        else
        {
            return null;
        }
    }

}
