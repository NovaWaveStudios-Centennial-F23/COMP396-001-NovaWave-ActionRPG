using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* FIREBALL STATS INDEX
 * 0 - Min Skill Damage
 * 1 - Max Skill Damage
 * 2 - Mana Cost
 * 3 - Cast Time
 * 4 - Cooldown
 * 5 - Duration
 * 6 - Radius
 * 7 - Projectile Speed
 * 8 - Range
 * 9 - Double Cast
 */

public class Fireball : MonoBehaviour
{
    public SkillSO fireball;
    public Vector3 direction;
    public float damage;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.stats[4].value);
        SkillsController.Instance.fireballCooldown = false;
    }

    void Start()
    {
        damage = Random.Range(fireball.stats[0].value, fireball.stats[1].value);
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Cooldown());
    }

    void Update()
    {
        ShootFireball();
    }

    private void ShootFireball()
    {
        transform.forward = (direction * 100) - transform.position;
        rb.velocity = transform.forward * fireball.stats[7].value;
    }

    private void ApplyBurning()
    {
        if (fireball.stats[9].value == 1)
        {
            //do burning
        }
    }
}
