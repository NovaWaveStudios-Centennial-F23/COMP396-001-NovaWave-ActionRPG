using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject outlineGO;

    [SerializeField]
    GameObject iconGO;

    [SerializeField]
    GameObject levelIndicatorGO;

    [SerializeField]
    public SkillTreeNodeSO nodeData;

    [Tooltip("Nodes that need to be allocated in order to allocate this one")]
    public List<SkillTreeNode> prerequisites = new List<SkillTreeNode>();

    //components: found at runtime
    Image image;
    Image outline;
    TextMeshProUGUI levelText;

    //variables
    public int maxLevel { get; private set; }
    public bool isActiveSkill { get; private set; }
    private bool isPlayerSkillTree;
    private int currentLevel = 0;
    List<SkillSO> dataObjects;
    //used for de-allocating skills, it will check dependancies before unallocating the skill
    private List<SkillTreeNode> dependancies = new List<SkillTreeNode>();

    //broadcast event whenenever current level of skill is changed
    public event Action<int> OnSkillLevelChanged = delegate { };

    private void Awake()
    {
        dataObjects = nodeData.skills;
    }

    private void Start()
    {
        maxLevel = dataObjects.Count;
        image = iconGO.GetComponent<Image>();
        outline = outlineGO.GetComponent<Image>();
        image.sprite = nodeData.icon;
        levelText = levelIndicatorGO.GetComponentInChildren<TextMeshProUGUI>();

        if (nodeData.skills[0].GetType() == typeof(ActiveSkillSO))
        {
            isActiveSkill = true;
        }
        else
        {
            isActiveSkill = false;
        }
        //auto-generate dependancies from prerequisites
        foreach(SkillTreeNode node in prerequisites)
        {
            node.AddDependancy(this);
        }

        if (nodeData.skillTreeType == "Player")
        {
            isPlayerSkillTree = true;
        }
        else
        {
            isPlayerSkillTree = false;
        }

        UpdateAppearance();
    }

    private void OnEnable()
    {
        UpdateAppearance();
    }
    void UpdateAppearance()
    {
        if (currentLevel > 0)
        {
            if(image == null)
            {
                image = iconGO.GetComponent<Image>();
            }

            if (outline == null)
            {
                outline = outlineGO.GetComponent<Image>();
            }

            image.color = UIConstants.selectedSkillColor;
            outline.color = UIConstants.selectedSkillOutline;

        }
        else
        {
            if (image == null)
            {
                image = iconGO.GetComponent<Image>();
            }

            if (outline == null)
            {
                outline = outlineGO.GetComponent<Image>();
            }

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

        //check if its possible to refund or deallocate skill point
        if(currentLevel > 0)
        {
            if (isPlayerSkillTree)
            {
                PlayerController.Instance.AddPlayerSkillPoints(1);
            }
            else
            {
                PlayerController.Instance.AddSkillSkillPoints(1);
            }
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
            if (isPlayerSkillTree)
            {
                if (PlayerController.Instance.SpendPlayerSkillPoints(1))
                {
                    currentLevel++;
                    //broadcasting event whenever skill level changes
                    OnSkillLevelChanged(currentLevel);
                }
            }
            else
            {
                if (PlayerController.Instance.SpendSkillSkillPoints(1))
                {
                    currentLevel++;
                    //broadcasting event whenever skill level changes
                    OnSkillLevelChanged(currentLevel);
                }
            }
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (PassedPrequisites())
            {
                PurchaseSkill();
                SkillTreeController.instance.RecalculateModifiers();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (currentLevel > 1 || PassedDependancies())
            {
                RefundSkill();
                SkillTreeController.instance.RecalculateModifiers();
            }
        }

        UpdateAppearance();

        ShowToolTip();
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
        if(currentLevel == 0)
        {
            return null;
        }
        else
        {
            Debug.Log(nodeData.skills[0]);
            Debug.Log(dataObjects[currentLevel - 1]);
            return dataObjects[currentLevel - 1].allStats;
        }       
    }

    public void AddDependancy(SkillTreeNode node)
    {
        dependancies.Add(node);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.Instance.CloseTooltips();

    }

    public SkillTreeNodeSO GetNodeData()
    {
        return nodeData;
    }

    public SkillSO GetCurrentSkillSO()
    {
        if(currentLevel > 0)
        {
            return GetSkillData(currentLevel-1);
        }
        else
        {
            return null;
        }
    }
    
    public SkillSO GetNextLevelSO()
    {
        if(currentLevel == maxLevel)
        {
            return null;
        }

        return GetSkillData(currentLevel);

    }

    private SkillSO GetSkillData(int index)
    {
        if (index < 0 || index > dataObjects.Count)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        return dataObjects[index];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(SkillTreeNode node in prerequisites)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    private void ShowToolTip()
    {
        if (isPlayerSkillTree)
        {
            ToolTipController.Instance.ShowPlayerSkillTooltip(this);
        }
        else
        {
            ToolTipController.Instance.ShowSkillToolTip(this);
        }
    }


    /// <summary>
    /// Only use this when loading the game!
    /// </summary>
    /// <param name="level"></param>
    public void SetLevel(int level)
    {
        currentLevel = level;
    }
}
