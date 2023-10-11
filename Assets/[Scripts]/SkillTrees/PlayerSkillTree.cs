using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTree : MonoBehaviour
{
    public int playerLvl;
    public PlayerStats player;
    
    public List<Stats> playerStats = new List<Stats>();
    public List<Node> playerNodes = new List<Node>();

    void Start()
    {
        playerStats = player.playerStats;
        UpdatePlayerStats();
    }

    public void UpdatePlayerStats()
    {
        foreach (Stats stat in playerStats)
        {
            foreach (Node node in playerNodes)
            {
                for (int i = 0; i < node.nodeStats.Count; i ++)
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

    public void UpgradePlayerNode(Node node)
    {
        node.nodeLvl += 1;
        UpdatePlayerStats();
    }

    public void DegradePlayerNode(Node node)
    {
        node.nodeLvl -= 1;
        UpdatePlayerStats();
    }
}
