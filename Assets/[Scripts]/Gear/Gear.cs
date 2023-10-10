using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GearSO gearSO;
    public List<Stats> gearStats;

    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter.mesh = gearSO.gearMeshFilter.mesh;

        //Converting Main Stats + Affixes into 1 list
        foreach (Stats s in gearSO.mainStats)
        {
            gearStats.Add(s);
        }
        foreach (Stats s in gearSO.affixes)
        {
            gearStats.Add(s);
        }
    }
}
