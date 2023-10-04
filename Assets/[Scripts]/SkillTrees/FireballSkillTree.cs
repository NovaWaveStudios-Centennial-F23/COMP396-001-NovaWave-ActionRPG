using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FIREBALL NODES INDEX
 * 0 - Base Fireball Level
 */

public class FireballSkillTree : MonoBehaviour
{
    public int fireballLvl;
    
    public List<Stats> fireballStats;
    public List<Nodes> fireballNodes = new List<Nodes>();

    private FireballSO fireballSO;

    private void Start()
    {
        LoadFireball();
        UpdateFireball();
    }

    public void LoadFireball()
    {
        fireballLvl = fireballNodes[0].nodeLvl;
        fireballSO = Resources.Load<FireballSO>("Skills/Fireball/Fireball" + fireballLvl.ToString());
        fireballStats = fireballSO.stats;
    }

    public void UpdateFireball()
    {
        for (int i = 0; i < fireballStats.Count; i++)
        {
            for (int j = 0; j < fireballNodes.Count; j++)
            {
                for (int k = 0; k < fireballNodes[j].nodeStat.Count; k++)
                {
                    if (fireballStats[i].stat == fireballNodes[j].nodeStat[k].stat)
                    {
                        fireballStats[i].value *= (fireballNodes[j].nodeLvl * fireballNodes[j].nodeStat[k].value);
                    }
                }
            }
        }
    }

    public void UpgradeNode(Nodes node)
    {
        node.nodeLvl += 1;
        if (node == fireballNodes[0])
        {
            LoadFireball();
        }

        UpdateFireball();
    }

    public void DegradeNode(Nodes node)
    {
        node.nodeLvl -= 1;
        if (node == fireballNodes[0])
        {
            LoadFireball();
        }
        UpdateFireball();
    }
}
