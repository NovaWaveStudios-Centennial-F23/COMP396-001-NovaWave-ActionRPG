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
                        float valueInc = playerStats[i].value * (playerNodes[j].nodeLvl * playerNodes[j].nodeStat[k].value);
                        playerStats[i].value += valueInc;                           
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
