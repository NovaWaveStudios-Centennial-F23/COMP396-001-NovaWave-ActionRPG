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

    [SerializeField]
    private float fireballSpeedInc;

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

    public void ToggleFireballDoubleCast()
    {
        fireballDoubleCast = !fireballDoubleCast;
    }

    public void ToggleBurning()
    {
        burning = !burning;
    }
}
