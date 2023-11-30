using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField]
    GameObject message;


    private void Start()
    {
        CloseMessage();
    }

    public void DontShowAgain()
    {
        PlayerPrefs.SetInt("ShowTutorial", 0);
        CloseMessage();
    }

    public void CloseMessage()
    {
        message.SetActive(false);
    }

    public void ShowMessage()
    {
        message.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMultiplayer>().isLocalPlayer)
            {
                if(PlayerPrefs.GetInt("ShowTutorial", 1) == 1)
                {
                    ShowMessage();
                }
                
            }
        }
    }

}
