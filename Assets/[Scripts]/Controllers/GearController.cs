using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    private static GearController instance;

    public static  GearController Instane { get { return instance; } }

    [SerializeField] private GearSO gearSO;
    [SerializeField] private GameObject gearPrefab;
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
        // Load Gear Scriptable Object
        gearSO = Resources.Load<GearSO>("Gear/" + gearType.ToString() + "/" + gearRarity.ToString() + gearBase.ToString());

        System.Random random = new System.Random();

        for (int i = 0; i < (int)gearType; i++)
        {
            int range = random.Next(0, gearSO.randomRolls.Count);
            gearSO.affixes.Add(gearSO.randomRolls[range]);
            gearSO.randomRolls.Remove(gearSO.randomRolls[range]); // No duplicate affixes
        }

        GameObject gear = Instantiate(gearPrefab, transform.position, Quaternion.identity);

        foreach (Stats s in gearSO.mainStats)
        {
            s.minValue = UnityEngine.Random.Range(s.minValue, s.maxValue);
            s.maxValue = s.minValue;
            gear.GetComponent<Gear>().GetGearStats().Add(s.stat, s);
        }
        foreach (Stats s in gearSO.affixes)
        {
            s.minValue = UnityEngine.Random.Range(s.minValue, s.maxValue);
            s.maxValue = s.minValue;
            gear.GetComponent<Gear>().GetGearStats().Add(s.stat, s);
        }

        gear.GetComponent<Gear>().InitGearStats();
    }

    public void GearDropCalculation()
    {
        // Number of possible drops
        int dropNumber = (int)Mathf.Round(UnityEngine.Random.Range(enemyStats.GetEnemyStat(Stats.Stat.Range).minValue, enemyStats.GetEnemyStat(Stats.Stat.Range).maxValue));

        // Drop Rate Stats
        float enemyDropRate = UnityEngine.Random.Range(enemyStats.GetEnemyStat(Stats.Stat.DropRateP).minValue, enemyStats.GetEnemyStat(Stats.Stat.DropRateP).maxValue);
        float playerDropRate = UnityEngine.Random.Range(playerStats.GetPlayerStat(Stats.Stat.DropRateP).minValue, playerStats.GetPlayerStat(Stats.Stat.DropRateP).maxValue);
        float dropRate = enemyDropRate + playerDropRate;

        // Item Rarity Stats
        float enemyItemRarity = UnityEngine.Random.Range(enemyStats.GetEnemyStat(Stats.Stat.ItemRarityP).minValue, enemyStats.GetEnemyStat(Stats.Stat.ItemRarityP).maxValue);
        float playerItemRarity = UnityEngine.Random.Range(playerStats.GetPlayerStat(Stats.Stat.ItemRarityP).minValue, playerStats.GetPlayerStat(Stats.Stat.ItemRarityP).maxValue);
        float itemRarity = enemyItemRarity + playerItemRarity;

        for (int i = 0; i < dropNumber; i++)
        {
            GearSO.GearType gearType;
            GearSO.GearBase gearBase;
            GearSO.GearRarity gearRarity;

            // Choose Gear Type
            System.Random random = new System.Random();
            int type = random.Next(0, 6);

            gearType = (GearSO.GearType)type;

            // Check for Drop rate
            float randomDropRate = UnityEngine.Random.value;
            float range = 238 * (Mathf.Exp(0.01f * dropRate) - 225);

            if (randomDropRate <= dropRate / 100)
            {
                float randomRarity = UnityEngine.Random.value;

                // Choose Gear Base
                if (randomDropRate <= (dropRate * 5) / 100)
                {
                    gearBase = GearSO.GearBase.Hero;
                }
                else if (randomDropRate > (dropRate * 5) / 100 && randomDropRate <= range / 100)
                {
                    gearBase = GearSO.GearBase.Warrior;
                }
                else
                {
                    gearBase = GearSO.GearBase.Basic;
                }

                // Choose Gear Rarity
                if (randomRarity <= itemRarity / 100)
                {
                    gearRarity = GearSO.GearRarity.Legendary;
                }
                else if (randomRarity > itemRarity && randomRarity <= (itemRarity * 5) / 100)
                {
                    gearRarity = GearSO.GearRarity.Rare;
                }
                else
                {
                    gearRarity = GearSO.GearRarity.Common;
                }

                SpawnGear(gearType, gearBase, gearRarity);
            }
        }
    }
}
