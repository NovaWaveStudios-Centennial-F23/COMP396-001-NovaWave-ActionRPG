/**Created by: Han Bi
 * A scene manager for the character selector scene
 * handles which screen to show and makes sure relevant keys are being captured
 */
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject characterSelectionPanel;

    [SerializeField]
    GameObject characterCreatorPanel;

    [SerializeField]
    GameObject multiplayerInputPanel;


    List<GameObject> allPanels = new List<GameObject>();

    private void Start()
    {
        allPanels.Clear();
        allPanels.Add(characterSelectionPanel);
        allPanels.Add(characterCreatorPanel);
        allPanels.Add(multiplayerInputPanel);

        CloseAllPanels();
        ShowCharacterSelection();
    }

    private void CloseAllPanels()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }
    }

    public void ShowCharacterSelection()
    {
        if (!characterSelectionPanel.activeInHierarchy)
        {
            CloseAllPanels();
            characterSelectionPanel.SetActive(true);
        }
    }

    public void ShowCharacterCreator()
    {
        if (!characterCreatorPanel.activeInHierarchy)
        {
            CloseAllPanels();
            characterCreatorPanel.SetActive(true);
        }
    }


    public void ShowMultiplayerOptions()
    {
        if (!multiplayerInputPanel.activeInHierarchy)
        {
            multiplayerInputPanel.SetActive(true);
        }
    }
    
    public void CloseMultiplayerOptions()
    {
        if (multiplayerInputPanel.activeInHierarchy)
        {
            multiplayerInputPanel.SetActive(false);
        }
        
    }



    





}
