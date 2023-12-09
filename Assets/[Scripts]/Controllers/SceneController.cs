using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : NetworkBehaviour
{

    public void LoadLevelOne()
    {
        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene("Level1");
        }
    }

    public void LoadTown()
    {
        
        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene("Town");
        }
    }


    public void LoadLevelTwo()
    {
        
        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene("Level2");
        }
    }


    public void LoadLevelThree()
    {
        
        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene("Level3");
        }
    }
}
