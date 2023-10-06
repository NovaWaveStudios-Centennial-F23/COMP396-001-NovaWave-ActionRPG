using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FIREBALL SKILLTREE NODES INDEX
 * 0 - Base Fireball Level
 * 1 - Mana Cost
 * 2 - Cast Time
 * 3 - Cooldown
 * 4 - Radius
 * 5 - Projectile Speed
 * 6 - Double Cast
 */

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
                        float valueInc = fireballStats[i].value * (fireballNodes[j].nodeLvl * fireballNodes[j].nodeStat[k].value);
                        fireballStats[i].value += valueInc;
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
