using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill/Active Skil")]
public class ActiveSkillSO : SkillSO
{
    public Stats manaCost = new(Stats.Stat.ManaCost);
    public Stats cooldown = new(Stats.Stat.Cooldown);

    protected override void OnValidate()
    {
        base.OnValidate();
        allStats.Add(manaCost);
        allStats.Add(cooldown);
    }
}
