using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }


}
