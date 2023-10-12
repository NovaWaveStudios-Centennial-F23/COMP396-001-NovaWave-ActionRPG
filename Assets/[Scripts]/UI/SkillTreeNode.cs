using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject outlineGO;

    [SerializeField]
    GameObject iconGO;

    [SerializeField]
    GameObject levelIndicatorGO;

    [SerializeField]
    List<SkillTreeNodeSO> dataObjects = new List<SkillTreeNodeSO>();

    [SerializeField]
    SkillTreeManager skillTreeGO;

    [Tooltip("Nodes that need to be allocated in order to allocate this one")]
    public List<SkillTreeNode> prerequisites = new List<SkillTreeNode>();

    

    //components: found at runtime
    Image image;
    Image outline;
    TextMeshProUGUI levelText;

    //variables
    private int maxLevel;
    private int currentLevel = 0;
    private List<SkillTreeNode> dependancies = new List<SkillTreeNode>();

    //broadcast event whenenever current level of skill is changed
    public event Action<int> OnSkillLevelChanged = delegate { };

    private void Start()
    {
        maxLevel = dataObjects.Count;
        image = iconGO.GetComponent<Image>();
        outline = outlineGO.GetComponent<Image>();
        image.sprite = dataObjects[0].icon;
        levelText = levelIndicatorGO.GetComponentInChildren<TextMeshProUGUI>();
        //auto-generate dependancies from prerequisites
        foreach(SkillTreeNode node in prerequisites)
        {
            node.AddDependancy(this);
        }

        UpdateAppearance();
    }
    void UpdateAppearance()
    {
        if (currentLevel > 0)
        {
            image.color = UIConstants.selectedSkillColor;
            outline.color = UIConstants.selectedSkillOutline;

        }
        else
        {
            image.color = UIConstants.deselectedSkillColor;
            outline.color = UIConstants.deselectedSkillOutline;
        }

        if (maxLevel > 1 && currentLevel > 0)
        {
            //show the level indicator
            levelIndicatorGO.SetActive(true);
            levelText.text = $"{currentLevel}/{maxLevel}";

        }
        else
        {
            //hide the level indicator
            levelIndicatorGO.SetActive(false);
        }       

        
    }

    private void RefundSkill()
    {
        //give player skillpoint

        //check if its possible to refund or deallocate skill point
        if(currentLevel > 0)
        {
            currentLevel--;
            //broadcasting event whenever skill level changes
            OnSkillLevelChanged(currentLevel);
        }
    }

    private void PurchaseSkill()
    {
        //take a skill point

        //check if its possible to add additional level into this skill
        if(currentLevel < maxLevel)
        {
            currentLevel++;
            //broadcasting event whenever skill level changes
            OnSkillLevelChanged(currentLevel);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (PassedPrequisites())
            {
                PurchaseSkill();
            }

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PassedDependancies() || currentLevel > 1)
            {
                RefundSkill();
            }
            
        }
        UpdateAppearance();
        CalculationController.Instance.SkillTreeCalculation(skillTreeGO.GetSkillTree());
    }

    private bool PassedPrequisites()
    {
        bool passedPrerequisites = true;

        if (prerequisites.Count > 0)
        {
            for (int i = 0; i < prerequisites.Count; i++)
            {
                if (prerequisites[i].GetCurrentLevel() < 1)
                {
                    passedPrerequisites = false;
                    break;
                }
            }
        }

        return passedPrerequisites;
    }

    private bool PassedDependancies()
    {
        bool passedDependancies = true;

        if(dependancies.Count > 0)
        {
            for(int i = 0;i < dependancies.Count; i++)
            {
                if (dependancies[i].GetCurrentLevel() > 0)
                {
                    passedDependancies=false;
                    break;
                }
            }

        }

        return passedDependancies;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public List<Stats> GetStats()
    {

        return dataObjects[currentLevel - 1].stats;

    }

    public void AddDependancy(SkillTreeNode node)
    {
        dependancies.Add(node);
    }

}
