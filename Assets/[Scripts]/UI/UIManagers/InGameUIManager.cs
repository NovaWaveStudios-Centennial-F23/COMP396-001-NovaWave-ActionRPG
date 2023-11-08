/** Created by Han Bi
 * Used to handle UI states for while in-game
 * Checks for input from player
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    InGameUIManager Instance;

    [SerializeField]
    GameObject inventoryPanel;

    [SerializeField]
    GameObject playerSkillTreePanel;

    [SerializeField]
    GameObject skillSkillTreePanel;

    bool listenForInputs = false;

    private List<GameObject> panels;


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
    }

    private void Start()
    {
        panels = new List<GameObject>() { inventoryPanel, playerSkillTreePanel, skillSkillTreePanel };
        SceneManager.activeSceneChanged += HandleSceneChange;
        listenForInputs = ShouldCheckForInput(SceneManager.GetActiveScene());
        CloseAllPanels();
    }

    private bool ShouldCheckForInput(Scene curScene)
    {
        return (curScene.buildIndex != 0 || curScene.buildIndex != 1);
    }

    private void HandleSceneChange(Scene prevScene, Scene curScene)
    {
        //hide all windows
        listenForInputs = ShouldCheckForInput(curScene);
        if(!listenForInputs)
        {
            //hide player HUD
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
            }
            else
            {
                CloseAllPanels();
                panel.SetActive(true);
            }
        }
    }

    private void CheckCloseAll()
    {
        if(Input.GetKeyDown(InputReferences.closeAllWindowsKey))
        {
            CloseAllPanels();
        }
    }

    private void CloseAllPanels()
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }

        ToolTipController.Instance.CloseTooltips();
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

}
