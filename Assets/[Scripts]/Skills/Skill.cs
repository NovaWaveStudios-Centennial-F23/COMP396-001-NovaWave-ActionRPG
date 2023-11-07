using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public ActiveSkillSO skillSO;
    public float cooldown;
    public List<GameObject> enemies;
    public SphereCollider AOE;
    public float damage;

    public abstract void SetIntitialValues();
    public abstract void MovementBehaviour();
    public abstract void CalculateCooldown();
    public abstract IEnumerator Duration();
}