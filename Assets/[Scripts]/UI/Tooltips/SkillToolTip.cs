/* Created by: Han Bi
 * Used to display details for skills
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillToolTip : ToolTipHandler
{
    [SerializeField]
    TextMeshProUGUI txtSkillName;

    [SerializeField]
    TextMeshProUGUI txtSkillDescription;

    [SerializeField]
    TextMeshProUGUI txtSkillLevel;


    public override void DisplayDetails(SkillTreeNode node)
    {
        int currentLevel = node.GetCurrentLevel();
        int maxLevel = node.maxLevel;
    }
}
