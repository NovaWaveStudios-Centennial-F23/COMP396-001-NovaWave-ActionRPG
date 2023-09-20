using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace NetworkingTest
{
    public class CanvasHUD : MonoBehaviour
    {
        public void StartHost()
        {
            NetworkManager.singleton.StartHost();
        }

        public void Back()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Join()
        {
            NetworkManager.singleton.StartClient();
        }
    }

}

