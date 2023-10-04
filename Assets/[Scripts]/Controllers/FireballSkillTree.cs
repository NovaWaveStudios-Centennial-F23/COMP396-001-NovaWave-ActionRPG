using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkillTree : MonoBehaviour
{
    public int baseFireballLvl;
    public bool burning;
    public bool fireballDoubleCast;

    [HideInInspector]
    public int fireballSpeedLvl;
    [HideInInspector]
    public int fireballCooldownLvl;

    [SerializeField]
    private float fireballSpeedInc;
    [SerializeField]
    private float fireballCooldownInc;

    private float fireballCooldown;
    private float fireballSpeed;
    private FireballSO fireballSO;

    private void Start()
    {
        LoadFireball();
    }

    public void LoadFireball()
    {
        fireballSO = Resources.Load<FireballSO>("Skills/Fireball/Fireball" + baseFireballLvl.ToString());
    }

    public float GetFireballSpeed()
    {
        fireballSpeed = (fireballSpeedLvl * fireballSpeedInc) + fireballSO.speed;
        return fireballSpeed;
    }

    public float GetFireballCooldown()
    {
        fireballCooldown = (fireballCooldownLvl * fireballCooldownInc) + fireballSO.coolDown;
        return fireballCooldown;
    }
}
