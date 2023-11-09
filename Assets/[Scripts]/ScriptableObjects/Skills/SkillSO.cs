
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
    public List<Stats> allStats { get { SynchronizeStats(); return _allStats; } set { _allStats = value; SynchronizeStats(); } }
    public List<Stats> miscStats = new List<Stats>();

    protected List<Stats> _allStats = new List<Stats>();

    protected virtual void OnValidate()
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
