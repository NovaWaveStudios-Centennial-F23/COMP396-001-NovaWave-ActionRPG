/* Created by Han Bi
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillNode", menuName = "ScriptableObjects/CreateSkillNode")]
public class SkillTreeNodeSO : ScriptableObject
{
    public List<SkillSO> skills;

    [SkillSelector]
    [SerializeField]
    string skillType;

    public string skillName;
    public Sprite icon;
    public string description;


    private void OnValidate()
    {

    }

}
