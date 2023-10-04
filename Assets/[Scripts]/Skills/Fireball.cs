using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* FIREBALL STATS INDEX
 * 0 - Min Skill Damage
 * 1 - Max Skill Damage
 * 2 - Cast Time
 * 3 - Cooldown
 * 4 - Duration
 * 5 - Radius
 * 6 - Projectile Speed
 * 7 - Range
 * 8 - Burning
 * 9 - Double Cast
 */

public class Fireball : MonoBehaviour
{
    public FireballSO fireball;
    public Vector3 direction;
    public float damage;

    private Rigidbody rb;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireball.stats[3].value);
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
        rb.velocity = transform.forward * fireball.stats[6].value;
    }

    private void ApplyBurning()
    {
        if (fireball.stats[8].value == 1)
        {
            //do burning
        }
    }
}
