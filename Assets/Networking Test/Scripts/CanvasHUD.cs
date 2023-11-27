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
        public Button buttonHost, buttonServer, buttonClient, buttonStop;

        public TMP_InputField inputFieldAddress;
        
        private void Start()
        {
            //Update the canvas text if you have manually changed network managers address from the game object before starting the game scene
            if (NetworkManager.singleton.networkAddress != "localhost") { inputFieldAddress.text = NetworkManager.singleton.networkAddress; }

            //Adds a listener to the main input field and invokes a method when the value changes.
            inputFieldAddress.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

            //Make sure to attach these Buttons in the Inspector
            buttonHost.onClick.AddListener(ButtonHost);
            buttonServer.onClick.AddListener(ButtonServer);
            buttonClient.onClick.AddListener(ButtonClient);
            buttonStop.onClick.AddListener(ButtonStop);

        }

        // Invoked when the value of the text field changes.
        public void ValueChangeCheck()
        {
            NetworkManager.singleton.networkAddress = inputFieldAddress.text;
        }

        public void ButtonHost()
        {
            NetworkManager.singleton.StartHost();

        }

        public void ButtonServer()
        {
            NetworkManager.singleton.StartServer();

        }

        public void ButtonClient()
        {
            NetworkManager.singleton.StartClient();
        }

        public void ButtonStop()
        {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopClient();
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                NetworkManager.singleton.StopServer();
            }

        }

   
    }

}

