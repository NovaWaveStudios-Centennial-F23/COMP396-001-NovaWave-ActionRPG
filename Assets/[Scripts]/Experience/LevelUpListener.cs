/**Created by Mithul Koshy
 * Used to listen level up events
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpListener : MonoBehaviour
{
    public ExperienceManager experienceManager;

    private void OnEnable()
    {
        experienceManager.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        experienceManager.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp()
    {
        Debug.Log("Player leveled up! Now at level: " + experienceManager.CurrentLevel);
        // Here you could add some effects, update UI, etc.
    }
}
