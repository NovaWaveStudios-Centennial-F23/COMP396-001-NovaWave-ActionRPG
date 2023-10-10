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
        for (int i = 0; i < playerStats.Count; i++)
        {
            for (int j = 0; j < playerNodes.Count; j++) 
            {
                for (int k = 0; k < playerNodes[j].nodeStat.Count; k++) 
                {
                    if (playerStats[i].stat == playerNodes[j].nodeStat[k].stat)
                    {
                        if (playerNodes[j].nodeType == Node.NodeType.Linear)
                        {
                            float valueInc = playerStats[i].statValue * (playerNodes[j].nodeLvl * playerNodes[j].nodeStat[k].statValue);
                            playerStats[i].statValue += valueInc;
                        }
                        else if (playerNodes[j].nodeType == Node.NodeType.Logarithmic)
                        {
                            float multiplier = (134 * Mathf.Log(0.13f + 1.9f) - 85) / playerNodes[j].nodeStat[k].statValue;
                            playerNodes[j].nodeValue = (134 * Mathf.Log((0.13f * playerNodes[j].nodeLvl) + 1.9f) - 85) * multiplier;
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
