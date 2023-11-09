/* Created by Han Bi
 * used for details about the skill tree node
 */
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SkillNode", menuName = "Scriptable Object/Skill Node")]
public class SkillTreeNodeSO : ScriptableObject
{
    public List<SkillSO> skills = new();

    [SkillSelector]
    public string skillTreeType;    

    public bool isActiveSkill { get; private set; }

    public string skillName;
    public Sprite icon;

    private void OnValidate()
    {
        //check skills list to see if this Node represents a passive skill or not
        if(skills.Count > 0)
        {
            if (skills[0].GetType() == typeof(ActiveSkillSO))
            {
                isActiveSkill = true;
            }
            else
            {
                isActiveSkill = false;
            }
        }
    }
}