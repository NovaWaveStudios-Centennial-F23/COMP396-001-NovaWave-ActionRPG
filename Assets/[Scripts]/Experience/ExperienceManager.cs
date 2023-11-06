/**Created by Mithul Koshy
 * Used to handle experience and level up for player
 */
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public event Action OnLevelUp; // Event for leveling up

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExperience { get; private set; } = 0;
    public int ExperienceToNextLevel { get; private set; } = 100;

    // Call this method when the player kills an enemy
    public void AddExperience(int amount)
    {
        CurrentExperience += amount;
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        while (CurrentExperience >= ExperienceToNextLevel)
        {
            CurrentExperience -= ExperienceToNextLevel;
            CurrentLevel++;
            ExperienceToNextLevel = CalculateExperienceForNextLevel(CurrentLevel);

            // Invoke the OnLevelUp event
            OnLevelUp?.Invoke();
        }
    }

    private int CalculateExperienceForNextLevel(int level)
    {
        return 100 * level;
    }
}
