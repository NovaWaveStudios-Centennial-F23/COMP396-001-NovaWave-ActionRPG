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
    public float nodeValue;
    public NodeType nodeType;
    public List<Stats> nodeStat = new List<Stats>();
}
