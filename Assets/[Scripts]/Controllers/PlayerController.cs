/**Created by Han Bi
 * Used to track changes in player information
 * Used as an API to get player information
 */


using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        if(Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public int PlayerSkillPoints { get; private set; }
    public int SkillSkillPoints {  get; private set; }

    public event Action<int> OnPlayerSkillPointsChange = delegate { };
    public event Action<int> OnSkillSkillPointsChange = delegate { };

    private void Start()
    {
        //for testing
        AddPlayerSkillPoints(10);
        AddSkillSkillPoints(5);
    }


    /// <summary>
    /// Attempts to spend the specified number of points. (spends 1 point by default)
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool SpendPlayerSkillPoints(int amount = 1)
    {
        if(PlayerSkillPoints >= amount)
        {
            PlayerSkillPoints -= amount;
            OnPlayerSkillPointsChange(PlayerSkillPoints);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to spend the specified number of points. (spends 1 point by default)
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool SpendSkillSkillPoints(int amount = 1)
    {
        if (SkillSkillPoints >= amount)
        {
            SkillSkillPoints -= amount;
            OnSkillSkillPointsChange(SkillSkillPoints);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddSkillSkillPoints(int amount)
    {
        SkillSkillPoints += amount;
        OnSkillSkillPointsChange(SkillSkillPoints);
    }

    public void AddPlayerSkillPoints(int amount)
    {
        PlayerSkillPoints += amount;
        OnPlayerSkillPointsChange(PlayerSkillPoints);
    }


}
