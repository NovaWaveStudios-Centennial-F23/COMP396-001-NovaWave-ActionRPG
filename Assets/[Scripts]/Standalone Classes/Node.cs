using System;
using System.Collections.Generic;

[System.Serializable]
public class Node
{
    public enum NodeType
    {
        Linear,
        Logarithmic,
        Exponential
    }
    public int nodeLvl;
    public int maxNodeLvl;
    public List<float> nodeValues;
    public NodeType nodeType;
    public List<Stats> nodeStats = new List<Stats>();
}
