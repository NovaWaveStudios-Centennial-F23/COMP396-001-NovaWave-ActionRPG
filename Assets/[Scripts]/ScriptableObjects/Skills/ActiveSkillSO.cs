
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill/Active Skil")]
public class ActiveSkillSO : SkillSO
{
    public Stats manaCost = new(Stats.Stat.ManaCost);
    public Stats cooldown = new(Stats.Stat.Cooldown);

    protected override void OnValidate()
    {
        SynchronizeStats();
    }

    protected override void SynchronizeStats()
    {
        base.SynchronizeStats();
        _allStats.Add(manaCost);
        _allStats.Add(cooldown);
    }
}
