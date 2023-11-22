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
    public string skillName;
    public Sprite icon;


}