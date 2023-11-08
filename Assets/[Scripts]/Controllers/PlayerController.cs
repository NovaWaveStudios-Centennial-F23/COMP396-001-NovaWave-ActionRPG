/**Created by Han Bi
 * Used to track changes in player information
 * Used as an API to get player information
 */

using System;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

/// <summary>
/// Used to track changes in player information
/// Handles changes to the player's state
/// </summary>
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

        ResetInfo();
    }

    public int CurrentLevel { get; private set; }
    public int PlayerSkillPoints { get; private set; }
    public int SkillSkillPoints {  get; private set; }

    public event Action<int> OnPlayerSkillPointsChange = delegate { };
    public event Action<int> OnSkillSkillPointsChange = delegate { };
    public event Action<int> OnLevelChange = delegate { };
    private void Start()
    {
        //for testing
        RefundPlayerSkillPoints(10);
        RefundSkillSkillPoints(5);

        ExperienceManager.Instance.OnLevelUp += HandleLevelUp;
    }


    /// <summary>
    /// Attempts to spend the specified number of points from the player controller. (spends 1 point by default)
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
    /// Attempts to spend the specified number of points from the player controller. (spends 1 point by default)
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

    /// <summary>
    /// Adds the specified amount of points to the player controller
    /// </summary>
    /// <param name="amount"></param>
    public void RefundSkillSkillPoints(int amount)
    {
        SkillSkillPoints += amount;
        OnSkillSkillPointsChange(SkillSkillPoints);
    }

    /// <summary>
    /// Adds the specified amount of points to the player controller
    /// </summary>
    /// <param name="amount"></param>
    public void RefundPlayerSkillPoints(int amount)
    {
        PlayerSkillPoints += amount;
        OnPlayerSkillPointsChange(PlayerSkillPoints);
    }


    //used to handle the level up event in Experience Manager
    private void HandleLevelUp()
    {
        CurrentLevel++;
        OnLevelChange(CurrentLevel);

        PlayerSkillPoints++;

        //gives a skill skill point every other level
        if(CurrentLevel % 2 == 0)
        {
            SkillSkillPoints++;
        }
    }


    void ResetInfo()
    {
        PlayerSkillPoints = 0;
        SkillSkillPoints = 1;
        CurrentLevel = 1;
    }


}
