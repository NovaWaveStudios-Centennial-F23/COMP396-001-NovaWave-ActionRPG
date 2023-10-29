/* Created by Han Bi
 * used for details about the skill tree node
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillNode", menuName = "Scriptable Object/Skill Node")]
public class SkillTreeNodeSO : ScriptableObject
{
    public List<SkillSO> skills = new List<SkillSO>();

    [SkillSelector]
    public string skillTreeType;

    public bool isActiveSkill { get; private set; }

    public string skillName;
    public Sprite icon;

    private void OnValidate()
    {
        //make sure that list of skills are of the same type
        if(skills.Count > 0)
        {
            Type t = skills[0].GetType();
            foreach(SkillSO skill in skills)
            {
                if(skill.GetType() != t)
                {
                    Debug.LogWarning($"Warning: there are different SkillTypes in the skill list of the {this.name} {nameof(SkillTreeNodeSO)}");
                }
            }
        }
    }
}
