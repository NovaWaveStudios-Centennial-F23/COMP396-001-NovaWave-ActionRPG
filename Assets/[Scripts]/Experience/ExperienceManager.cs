/**Created by Mithul Koshy
 * Used to handle experience and level up for player
 */
using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance {get; private set;}
    public event Action OnLevelUp; // Event for leveling up
    public event Action<int> OnExpGain;

    [SerializeField]
    AudioClip levelupClip;

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExperience { get; private set; } = 0;
    public int ExperienceToNextLevel { get; private set; } = 100;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }


    // Call this method when the player kills an enemy
    public void AddExperience(int amount)
    {
        CurrentExperience += amount;
        OnExpGain(CurrentExperience);
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

            //play sound effect
            AudioController.Instance.PlayOneShot(levelupClip);
        }
    }

    private int CalculateExperienceForNextLevel(int level)
    {
        return 100 * level;
    }
}
