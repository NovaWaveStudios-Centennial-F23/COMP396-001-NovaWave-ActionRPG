using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkillTree : MonoBehaviour
{    
    public List<Stats> fireballStats = new List<Stats>();
    public List<Node> fireballNodes = new List<Node>();

    [HideInInspector]
    public int fireballLvl;

    private SkillSO fireballSO;

    private void Start()
    {
        LoadFireball();
        UpdateFireballStats();
    }

    public void LoadFireball()
    {
        fireballLvl = fireballNodes[0].nodeLvl;
        fireballSO = Resources.Load<SkillSO>("Skills/Fireball/Fireball" + fireballLvl.ToString());
        fireballStats = fireballSO.stats;
    }

    public void UpdateFireballStats()
    {
        for (int i = 0; i < fireballStats.Count; i++)
        {
            for (int j = 0; j < fireballNodes.Count; j++)
            {
                for (int k = 0; k < fireballNodes[j].nodeStat.Count; k++)
                {
                    if (fireballStats[i].stat == fireballNodes[j].nodeStat[k].stat)
                    {
                        if (fireballNodes[j].nodeType == Node.NodeType.Linear)
                        {
                            float valueInc = fireballStats[i].statValue * (fireballNodes[j].nodeLvl * fireballNodes[j].nodeStat[k].statValue);
                            fireballStats[i].statValue += valueInc;
                        }
                        else if (fireballNodes[j].nodeType == Node.NodeType.Logarithmic)
                        {
                            float multiplier = (134 * Mathf.Log(0.13f + 1.9f) - 85) / fireballNodes[j].nodeStat[k].statValue;
                            fireballNodes[j].nodeValue = (134 * Mathf.Log((0.13f * fireballNodes[j].nodeLvl) + 1.9f) - 85) * multiplier;
                        }
                    }
                }
            }
        }
    }

    public void UpgradeFireballNode(Node node)
    {
        node.nodeLvl += 1;
        if (node == fireballNodes[0])
        {
            LoadFireball();
        }

        UpdateFireballStats();
    }

    public void DegradeFireballNode(Node node)
    {
        node.nodeLvl -= 1;
        if (node == fireballNodes[0])
        {
            LoadFireball();
        }
        UpdateFireballStats();
    }
}
