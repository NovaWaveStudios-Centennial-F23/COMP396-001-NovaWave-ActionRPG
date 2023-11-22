using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillSciptableObject", menuName = "ScriptableObejcts/Create New Skill/Active Skil")]
public class ActiveSkillSO : SkillSO
{
    public Stats manaCost = new(Stats.Stat.ManaCost);
    public Stats cooldown = new(Stats.Stat.Cooldown);
    public Stats duration = new(Stats.Stat.Duration);
    public Stats AOE = new(Stats.Stat.AOE);

    protected override void OnValidate()
    {
        base.Awake();
        allStats.Add(manaCost);
        allStats.Add(cooldown);
        allStats.Add(duration);
        allStats.Add(AOE);
    }

    protected override void Awake()
    {
        base.Awake();
        allStats.Add(manaCost);
        allStats.Add(cooldown);
        allStats.Add(duration);
        allStats.Add(AOE);
    }
}
