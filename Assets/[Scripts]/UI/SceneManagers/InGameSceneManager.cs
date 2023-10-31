/** Created by Han Bi
 * Used to handle UI states for while in-game
 */
using Unity.VisualScripting;
using UnityEngine;

public class InGameSceneManager : MonoBehaviour
{
    InGameSceneManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        
    }


    //handle inventory

    //handle skill skilltree

    //handle player skilltree


}
