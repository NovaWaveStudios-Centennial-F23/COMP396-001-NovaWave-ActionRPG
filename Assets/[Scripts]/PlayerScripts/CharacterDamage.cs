using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    public ValuePool lifepool; // Assuming this is your character's health pool

    // This method is used to apply damage to the character
    public void TakeDamage(float damageAmount)
    {
        if (lifepool == null)
        {
            lifepool = new ValuePool();
            lifepool.maxValue = 100;
            lifepool.currentValue = 100; // Initialize currentValue to the maximum value
        }

        // Reduce the currentValue of the lifepool by the damageAmount
        lifepool.currentValue -= damageAmount;
        Debug.Log("Lifepool: " + lifepool.currentValue);
    }
}
