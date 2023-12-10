using Mirror;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathPanel; // Reference to the death panel

    // Singleton instance
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Implementing a basic singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Hide the death panel at the start
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    public void ShowDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    public void HideDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    public void Teleport()
    {
        HideDeathPanel();
        LoadTown();
    }

    public void LoadTown()
    {

            NetworkManager.singleton.ServerChangeScene("Town");

    }
}
