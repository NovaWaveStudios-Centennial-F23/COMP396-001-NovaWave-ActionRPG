using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMultiplayer : NetworkBehaviour
{
    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject projectileSpawner;

    private void Start()
    {
        if (isLocalPlayer)
        {
            //allow the camera to be created
            playerCamera.SetActive(true);

            //register self with the skill controller
            SkillsController.Instance.Init(gameObject, projectileSpawner);
        }
        else
        {
            playerCamera.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    
    


}
