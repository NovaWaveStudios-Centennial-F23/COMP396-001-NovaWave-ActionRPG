using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireballScriptableObject", menuName = "ScriptableObejcts/Skills/Fireball")]
public class FireballSO: SkillSO
{
    public float speed;
    public float range;
    public bool doubleCast;
    public bool burning;
}
