using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GearSO gearSO;

    private MeshFilter meshFilter;
    void Start()
    {
        meshFilter.mesh = gearSO.gearMeshFilter.mesh;
    }
}
