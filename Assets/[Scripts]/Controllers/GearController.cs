using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    private static GearController instance;

    public static  GearController Instane { get { return instance; } }

    [SerializeField] private GearSO gearSO;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EnemyStatsTest enemyStats;
    //test variables


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void SpawnGear(GearSO.GearType gearType, GearSO.GearBase gearBase, GearSO.GearRarity gearRarity)
    {

    }

    public void GearDrops(GearSO.GearType gearType)
    {
        int dropNumber = (int)Mathf.Round(Random.Range(enemyStats.GetEnemyStat(Stats.Stat.Range).minValue, enemyStats.GetEnemyStat(Stats.Stat.Range).maxValue));

        float enemyDropRate = Random.Range(enemyStats.GetEnemyStat(Stats.Stat.DropRateP).minValue, enemyStats.GetEnemyStat(Stats.Stat.DropRateP).maxValue);
        float playerDropRate = Random.Range(playerStats.GetPlayerStat(Stats.Stat.DropRateP).minValue, playerStats.GetPlayerStat(Stats.Stat.DropRateP).maxValue);
        float dropRate = enemyDropRate + playerDropRate;

        float enemyItemRarity = Random.Range(enemyStats.GetEnemyStat(Stats.Stat.ItemRarityP).minValue, enemyStats.GetEnemyStat(Stats.Stat.ItemRarityP).maxValue);
        float playerItemRarity = Random.Range(playerStats.GetPlayerStat(Stats.Stat.ItemRarityP).minValue, playerStats.GetPlayerStat(Stats.Stat.ItemRarityP).maxValue);
        float itemRarity = enemyItemRarity + playerItemRarity;

        for (int i = 0; i < dropNumber; i++)
        {
            float randomDropRate = Random.value;
            float range = 238 * (Mathf.Exp(0.01f * dropRate) - 225);

            if (randomDropRate <= dropRate / 100)
            {
                float randomRarity = Random.value;

                if (randomRarity <= itemRarity / 100)
                {
                    // Drops legendary gear
                    if (randomDropRate <= (dropRate * 5) / 100)
                    {
                        // Hero
                        SpawnGear(gearType, GearSO.GearBase.Hero, GearSO.GearRarity.Legendary);
                    }
                    else if (randomDropRate > (dropRate * 5) / 100 && randomDropRate <= range / 100)
                    {
                        // Warrior
                        SpawnGear(gearType, GearSO.GearBase.Warrior, GearSO.GearRarity.Legendary);
                    }
                    else
                    {
                        // Basic
                        SpawnGear(gearType, GearSO.GearBase.Basic, GearSO.GearRarity.Legendary);
                    }
                }
                else if (randomRarity > itemRarity && randomRarity <= (itemRarity * 5) / 100)
                {
                    //Drops rare gear
                    if (randomDropRate <= (dropRate * 5) / 100)
                    {
                        // Hero
                        SpawnGear(gearType, GearSO.GearBase.Hero, GearSO.GearRarity.Rare);
                    }
                    else if (randomDropRate > (dropRate * 5) / 100 && randomDropRate <= range / 100)
                    {
                        // Warrior
                        SpawnGear(gearType, GearSO.GearBase.Warrior, GearSO.GearRarity.Rare);
                    }
                    else
                    {
                        // Basic
                        SpawnGear(gearType, GearSO.GearBase.Basic, GearSO.GearRarity.Rare);
                    }
                }
                else
                {
                    //Drops common gear
                    if (randomDropRate <= (dropRate * 5) / 100)
                    {
                        // Hero
                        SpawnGear(gearType, GearSO.GearBase.Hero, GearSO.GearRarity.Common);
                    }
                    else if (randomDropRate > (dropRate * 5) / 100 && randomDropRate <= range / 100)
                    {
                        // Warrior
                        SpawnGear(gearType, GearSO.GearBase.Warrior, GearSO.GearRarity.Common);
                    }
                    else
                    {
                        // Basic
                        SpawnGear(gearType, GearSO.GearBase.Basic, GearSO.GearRarity.Common);
                    }
                }
            }
        }
    }
}
