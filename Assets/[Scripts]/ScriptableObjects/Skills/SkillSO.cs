
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill")]
public class SkillSO : ScriptableObject
{
    public enum DamageType
    {
        Normal,
        Fire,
        Frost,
        Electro,
        None
    }

    public enum SkillType
    {
        Projectile,
        OnPlayer,
        OnMouse
    }

    public GameObject prefab;
    public DamageType damageType;
    public SkillType skillType;

    public List<Stats> miscStats = new List<Stats>();
    protected List<Stats> _allStats = new List<Stats>();

    public List<Stats> allStats
    {
        get
        {
            if(_allStats.Count == 0)
            {
              SynchronizeStats();
            }
            
            return _allStats;
        }

        set { _allStats = value; }
    }

    protected virtual void Awake()
    {
        SynchronizeStats();
    }

    protected virtual void SynchronizeStats()
    {
        _allStats.Clear();

        foreach (Stats stat in miscStats)
        {
            _allStats.Add(stat);
        }
    }
}
