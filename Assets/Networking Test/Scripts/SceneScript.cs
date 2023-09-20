using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NetworkingTest
{
    public class SceneScript : NetworkBehaviour
    {
        public TextMeshProUGUI canvasStatusText;
        public NetworkPlayerScript1 playerScript;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

        public SceneReference sceneReference;

        public TextMeshProUGUI canvasAmmoText;

        void OnStatusTextChanged(string _old, string _new)
        {
            canvasStatusText.text = statusText;
        }

        public void ButtonSendMessage()
        {
            if (playerScript != null)
            {
                playerScript.CmdSendPlayerMessage();
            }
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "NetworkTest1")
                    NetworkManager.singleton.ServerChangeScene("NetworkTest2");
                else
                    NetworkManager.singleton.ServerChangeScene("NetworkTest1");
            }
            else
                Debug.Log("You are not Host.");
        }

        public void UIAmmo(int _value)
        {
            canvasAmmoText.text = "Ammo: " + _value;
        }


    }


}
