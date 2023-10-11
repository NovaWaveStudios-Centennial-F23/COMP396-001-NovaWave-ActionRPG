using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillNode", menuName = "ScriptableObjects/CreateSkillNode")]
public class SkillTreeNodeSO : ScriptableObject
{
    public List<Stats> stats = new List<Stats>();
    public Sprite icon;

    //only used if there is are prequisites for the skill
    public List<SkillTreeNodeSO> dependancies = new List<SkillTreeNodeSO>();

}
