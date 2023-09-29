using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace NetworkingTest
{
    public class CanvasHUD : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI ipInput;
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
            NetworkManager.singleton.networkAddress = ipInput.text;
            NetworkManager.singleton.StartClient();
        }
    }

}

