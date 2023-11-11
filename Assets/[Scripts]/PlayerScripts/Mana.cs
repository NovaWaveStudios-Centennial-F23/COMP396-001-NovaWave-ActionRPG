/**Created by: Han Bi
 * Represents the mana of the player
 * Handles things related 
 */
using System;
using UnityEngine;

public class Mana : MonoBehaviour
{
    private ValuePool manaPool;
    public event Action<ValuePool> PoolChanged;

    [SerializeField] float manaRegen; 
    //will need to get this value from calculation controller

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Need some way of getting the max mana the player should have.

        manaPool = new ValuePool { maxValue = 100f, currentValue = 100f };
    }

    private void Update()
    {
        if(manaPool.currentValue < manaPool.maxValue)
        {
            GainMana(manaRegen * Time.deltaTime);
        }
    }

    /// <summary>
    /// Used when taking mana away, will return true if successful
    /// </summary>
    /// <param name="value">Cannot be negative</param>
    /// <returns></returns>
    public bool SpendMana(float value)
    {
        //check for invalid values or if there is enough mana
        if(value < 0 || manaPool.currentValue < value)
        {
            return false;
        }
        else
        {
            manaPool.currentValue -= value;
            PoolChanged(manaPool);
            return true;
        }
    }

    /// <summary>
    /// Used for giving the player mana
    /// </summary>
    /// <param name="value">Cannot be negative</param>
    public void GainMana(float value)
    {
        if(value > 0)
        {
            manaPool.currentValue = Mathf.Clamp(manaPool.currentValue + value, 0, manaPool.maxValue);
            PoolChanged(manaPool);
        }
    }

}
