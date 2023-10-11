using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connector : MonoBehaviour
{
    [SerializeField]
    Image outerLine;

    [SerializeField]
    Image innerLine;

    private SkillTreeNode skillNode;

    //observer
    private void HandleSkillLevelChange(int skillLevel)
    {
        if(skillLevel > 0)
        {
            TurnActive();
        }
        else
        {
            TurnInactive();
        }
    }

    private void Initialize()
    {
        if (skillNode != null)
        {
            skillNode.OnSkillLevelChanged += HandleSkillLevelChange;
        }
    }

    private void OnDestroy()
    {
        if (skillNode != null)
        {
            skillNode.OnSkillLevelChanged -= HandleSkillLevelChange;
        }
    }

    private void Awake()
    {
        TurnInactive();
    }

    private void TurnActive()
    {
        outerLine.color = UIConstants.activeConnectorOutline;
        innerLine.color = UIConstants.activeConnectorInner;
    }

    private void TurnInactive()
    {
        outerLine.color = UIConstants.inactiveConnectorOutline;
        innerLine.color = UIConstants.inactiveConnectorInner;
    }

    public void SetSkillNode(SkillTreeNode node)
    {
        //maybe not neccessary to check but I don't want to risk subscribing to multiple instances of this class
        if(skillNode == null)
        {
            skillNode = node;
            Initialize();
            //to prevent issues where the value of the node has changed before we are subscribed to it, I will force an update
            HandleSkillLevelChange(skillNode.GetCurrentLevel());
        }
        
        
    }



}
