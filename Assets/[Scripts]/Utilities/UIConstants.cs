/* Created by: Han Bi
 * Used for UI settings like display colour
 * Also used for deciding how to display stats for spells and skill tree nodes
 */

using UnityEngine;

public static class UIConstants
{
    //colour for the skill icon
    public static readonly Color deselectedSkillColor = new Color(0.6415094f, 0.6415094f, 0.6415094f);
    public static readonly Color selectedSkillColor = new Color(1, 1, 1);

    //colours for outline of the skill
    public static readonly Color deselectedSkillOutline = new Color(0, 0, 0);
    public static readonly Color selectedSkillOutline = new Color(244, 140, 67);

    //connector colours
    public static readonly Color inactiveConnectorOutline = new Color(0, 0, 0);
    public static readonly Color inactiveConnectorInner = new Color(0, 0, 0);

    public static readonly Color activeConnectorOutline = new Color(0, 0, 0);
    public static readonly Color activeConnectorInner = new Color(0.3593603f, 1, 0);

    //Character selection colours
    public static readonly Color unselectedCharacterBorderColour = new Color(0.03529412f, 0.5215687f, 0.003921569f);
    public static readonly Color selectedCharacterBorderColour = new Color(1, 1, 1);

    public static string SpellStringify(Stats stat)
    {
        string desc = "";
        int average = Mathf.RoundToInt((stat.minValue + stat.maxValue) / 2);
        int percentage = Mathf.RoundToInt((stat.minValue + stat.maxValue) / 0.02f);

        switch (stat.stat)
        {
            case Stats.Stat.BaseDamage:
                desc = $"deals increased {stat.minValue} to {stat.maxValue} of your base damage";
                break;
            default:
                Debug.LogWarning($"{stat.stat} does not have a corresponding description in spells");
                break;
        }

        return desc;
    }


}
