/**Created by Han Bi
 * Used to track changes in player information
 * Used as an API to get player information
 */


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerController Instance;

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
            return true;
        }
        else
        {
            return false;
        }
    }


}
