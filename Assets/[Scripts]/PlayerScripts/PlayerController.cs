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

}
