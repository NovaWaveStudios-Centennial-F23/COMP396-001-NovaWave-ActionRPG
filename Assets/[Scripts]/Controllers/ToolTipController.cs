/* Created by: Han Bi
 * Used for displaying UI when user hovers over a interactive object
 * Singleton design pattern since there should only be one tooltip on screen at any given time
 */
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    public static ToolTipController Instance { get;  private set; }

    [SerializeField]
    GameObject skillTooltip;

    [SerializeField]
    GameObject nodeToolTip;

    [SerializeField]
    GameObject gearToolTip;

    private List<GameObject> toolTips;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        toolTips = new List<GameObject>
        {
            skillTooltip,
            nodeToolTip,
            gearToolTip
        };

        CloseTooltips();
    }

    public void ShowSkillToolTip(SkillTreeNode node)
    {
        CloseTooltips();
        skillTooltip.SetActive(true);
        skillTooltip.GetComponent<SkillToolTip>().DisplayDetails(ref node);
        Vector2 mousePos = Input.mousePosition;
        skillTooltip.transform.position = mousePos;
    }




    public void CloseTooltips()
    {
        foreach(GameObject tip in toolTips) {            
            tip.SetActive(false);
        }
    }









}
