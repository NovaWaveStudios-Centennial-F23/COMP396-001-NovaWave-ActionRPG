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
        foreach (Stats stat in fireballStats)
        {
            foreach (Node node in fireballNodes)
            {
                for (int i = 0; i < node.nodeStats.Count; i++)
                {
                    if (stat.stat == node.nodeStats[i].stat)
                    {
                        if (node.nodeType == Node.NodeType.Linear)
                        {
                            node.nodeValues[i] = node.nodeLvl * node.nodeStats[i].statValue;
                        }
                        else if (node.nodeType == Node.NodeType.Logarithmic)
                        {
                            float multiplier = node.nodeStats[i].statValue / (134 * Mathf.Log(0.13f + 1.9f) - 85);
                            node.nodeValues[i] = (134 * Mathf.Log((0.13f * node.nodeLvl) + 1.9f) - 85) * multiplier;
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
