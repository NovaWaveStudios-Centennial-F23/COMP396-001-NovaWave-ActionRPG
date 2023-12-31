/**Created by Han Bi
 * Used to track changes in player information
 * Used as an API to get player information
 */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //ResetPlayerInfo();

        SceneManager.sceneUnloaded += HandleSceneUnload;
        SceneManager.sceneLoaded += HandleSceneLoad;
    }

    private void HandleSceneUnload(Scene scene)
    {
        if(ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnLevelUp -= HandleLevelUp;
        }
    }

    private void HandleSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnLevelUp += HandleLevelUp;
        }
        
    }

    public int CurrentLevel { get; set; }
    public int PlayerSkillPoints { get; set; }
    public int SkillSkillPoints {  get; set; }

    public event Action<int> OnPlayerSkillPointsChange = delegate { };
    public event Action<int> OnSkillSkillPointsChange = delegate { };
    public event Action<int> OnLevelChange = delegate { };

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
    public void AddSkillSkillPoints(int amount)
    {
        SkillSkillPoints += amount;
        OnSkillSkillPointsChange(SkillSkillPoints);
    }

    /// <summary>
    /// Adds the specified amount of points to the player controller
    /// </summary>
    /// <param name="amount"></param>
    public void AddPlayerSkillPoints(int amount)
    {
        PlayerSkillPoints += amount;
        OnPlayerSkillPointsChange(PlayerSkillPoints);
    }


    //used to handle the level up event in Experience Manager
    private void HandleLevelUp()
    {
        CurrentLevel++;
        OnLevelChange(CurrentLevel);

        //PlayerSkillPoints++;

        SkillSkillPoints++;
    }


    public void ResetPlayerInfo()
    {
        //dummy data for now
        PlayerSkillPoints = 0;
        SkillSkillPoints = 3;
        CurrentLevel = 1;
    }


}
