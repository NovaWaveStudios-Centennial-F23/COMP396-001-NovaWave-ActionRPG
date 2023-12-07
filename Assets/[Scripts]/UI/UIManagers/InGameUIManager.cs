/** Created by Han Bi
 * Used to handle UI states for while in-game
 * Checks for input from player
 */
using Mirror;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    [SerializeField]
    GameObject inventoryPanel;

    [SerializeField]
    GameObject playerSkillTreePanel;

    [SerializeField]
    GameObject skillSkillTreePanel;

    [SerializeField]
    GameObject ingameMenuPanel;

    [SerializeField]
    GameObject playerHUDPanel;

    bool listenForInputs = false;

    //list of all panels that are 'optional' (does not include UI that 'always' exists)
    private List<GameObject> panels;

    //temporary
    public UIPoolBar playerHealthBar;
    public ManaBar playerManaBar;
    public UIPoolBar enemyHealthBarDisplay;
    public TextMeshProUGUI txtEnemyName;

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

        SceneManager.sceneLoaded += HandleSceneChange;
    }

    void HandleSceneChange(Scene currScene, LoadSceneMode mode)
    {
        Start();
    }

    private void Start()
    {
        if(panels == null)
        {
            panels = new List<GameObject>() { inventoryPanel, playerSkillTreePanel, skillSkillTreePanel, ingameMenuPanel };
        }
        listenForInputs = ShouldCheckForInput(SceneManager.GetActiveScene());
        // generate inventory slots
        inventoryPanel.SetActive(true);
        CloseAllPanels();
        if(ingameMenuPanel != null)
        {
            ingameMenuPanel.SetActive(false);
        }
        
        if(playerHUDPanel != null)
        {
            playerHUDPanel.SetActive(listenForInputs);
        }
        
    }

    private bool ShouldCheckForInput(Scene curScene)
    {
        if(curScene.buildIndex == 0)
        {
            return false;
        }else if(curScene.buildIndex == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Update()
    {
        if (listenForInputs)
        {
            CheckForInput(inventoryPanel, InputReferences.inventoryKey);
            CheckForInput(playerSkillTreePanel, InputReferences.playerSkillTreeKey);
            CheckForInput(skillSkillTreePanel, InputReferences.skillSkillTreeKey);
            CheckCloseAll();
        }

    }

    //generic function that checks if key is pressed and if so, toggles the appropriate UI
    private void CheckForInput(GameObject panel, KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
                ToolTipController.Instance.CloseTooltips();
            }
            else
            {
                CloseAllPanels();
                panel.SetActive(true);
            }
        }
    }

    bool AllWindowsClosed()
    {
        foreach(var panel in panels)
        {
            if(panel.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }

    private void CheckCloseAll()
    {
        if(Input.GetKeyDown(InputReferences.closeAllWindowsKey))
        {
            //if all windows are already closed
            if (AllWindowsClosed())
            {
                ToggleInGameMenu();
            }
            else
            {
                CloseAllPanels();
            }
            
            
        }
    }

    private void CloseAllPanels()
    {
        if(panels != null)
        {
            foreach (GameObject p in panels)
            {
                if(p != null)
                {
                    p.SetActive(false);
                }
                
            }
        }

        if(ToolTipController.Instance != null)
        {
            ToolTipController.Instance.CloseTooltips();
        }
        
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        ToolTipController.Instance.CloseTooltips();
    }

    public void ClosePlayerSkillTree()
    {
        playerSkillTreePanel.SetActive(false);
        ToolTipController.Instance.CloseTooltips();
    }

    public void CloseSkillSkillTree()
    {
        skillSkillTreePanel.SetActive(false);
        ToolTipController.Instance.CloseTooltips();
    }

    public void ToggleInventory()
    {
        
        if (!inventoryPanel.activeInHierarchy)
        {
            CloseAllPanels();
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
        
    }

    public void TogglePlayerSkillTree()
    {
        if (!playerSkillTreePanel.activeInHierarchy)
        {
            CloseAllPanels();
            playerSkillTreePanel.SetActive(true);
        }
        else
        {
            playerSkillTreePanel.SetActive(false);
        }
    }

    public void ToggleSkillTree()
    {
        if (!skillSkillTreePanel.activeInHierarchy)
        {
            CloseAllPanels();
            skillSkillTreePanel.SetActive(true);
        }
        else
        {
            skillSkillTreePanel.SetActive(false);
        }
    }

    public void ToggleInGameMenu()
    {
        if (ingameMenuPanel.activeInHierarchy)
        {
            ingameMenuPanel.SetActive(false);
        }
        else
        {
            CloseAllPanels();
            ingameMenuPanel.SetActive(true);
        }
    }

    public void MainMenu()
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

        SceneManager.LoadScene("StartMenu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
