/* Created by: Han Bi
 * Used for displaying UI when user hovers over a interactive object
 * Singleton design pattern since there should only be one tooltip on screen at any given time

 * Yusuke Kuroki: Added ShowGearTooltip
 */
using System.Collections.Generic;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    public static ToolTipController Instance;

    [SerializeField]
    GameObject skillTooltip;

    [SerializeField]
    GameObject playerSkillTooltip;

    [SerializeField]
    GameObject nodeTooltip;

    [SerializeField]
    GameObject gearTooltip;

    private List<GameObject> toolTips = new();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {

        toolTips = new List<GameObject>
        {
            skillTooltip,
            playerSkillTooltip,
            nodeTooltip,
            gearTooltip
        };

        CloseTooltips();
    }

    public void ShowSkillToolTip(SkillTreeNode node)
    {
        CloseTooltips();
        skillTooltip.SetActive(true);
        skillTooltip.GetComponent<SkillToolTip>().DisplayDetails(node);
    }

    public void CloseTooltips()
    {
        foreach(GameObject t in toolTips) {
            t.SetActive(false);
        }
    }

    public void ShowPlayerSkillTooltip(SkillTreeNode node)
    {
        CloseTooltips();
        playerSkillTooltip.SetActive(true);
        playerSkillTooltip.GetComponent<SkillToolTip>().DisplayDetails(node);
        
    }

    public void ShowGearTooltip(GearSO gear)
    {
        CloseTooltips();
        gearTooltip.SetActive(true);
        gearTooltip.GetComponent<GearToolTip>().DisplayDetails(gear);
    }
}
