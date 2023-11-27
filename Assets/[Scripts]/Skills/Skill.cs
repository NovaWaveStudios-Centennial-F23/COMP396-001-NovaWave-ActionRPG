using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : NetworkBehaviour
{
    public ActiveSkillSO skillSO;
    public List<GameObject> enemies;
    public SphereCollider AOE;
    public float damage;
    public float cooldown;
    [SyncVar]
    public float aoe;

    public abstract void SetIntitialValues();
    public abstract void MovementBehaviour();
    public abstract void CalculateCooldown();
    public abstract IEnumerator Duration();
}