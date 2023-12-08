/**Created by: Han Bi
 * Used to handle character save data
 * 
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    Button btnMultiplayer;

    [SerializeField]
    Button btnSingleplayer;

    [SerializeField]
    Button btnCreateCharacter;

    [SerializeField]
    Button btnDeleteCharacter;

    [SerializeField]
    TextMeshProUGUI txtHeading;

    [SerializeField]
    GameObject characterOptionPrefab;

    [SerializeField]
    CharacterSelectionHandler characterSelectionHandler;

    [SerializeField]
    bool generateFakeData;

    int characterCount = 0;

    public GameObject currentSelection;


    private void Awake()
    {
        SetSelection(null);
        Initialize();
    }


    private void OnDisable()
    {
        SetSelection(null);
    }

    private void Initialize()
    {
        characterCount = 0;

        DisplayData();

        var characters = GetComponentInChildren<CharacterSelectButton>();
        txtHeading.text = $"Characters: {characterCount}/4";
    }


    //given the save data, display the details
    private void DisplayData()
    {
        //parse save data
    }


    //generates some fake data for the chracter selector
    public void GenerateData(string name, int level, int saveNumber)
    {
        CharacterSaveData s1 = new CharacterSaveData(name, level, saveNumber);
        var obj1 = Instantiate(characterOptionPrefab);
        obj1.transform.SetParent(transform, false);
        obj1.GetComponent<CharacterSelectButton>().PopulateData(s1);
        obj1.GetComponent<CharacterSelectButton>();
        characterSelectionHandler.AddButton(obj1.GetComponent<CharacterSelectButton>());
        characterCount++;
    }


    public void SetSelection(GameObject character)
    {
        currentSelection = character;
        UpdateButtons();
    }

    //this sets the state of the buttons making sure only the right buttons are enabled
    private void UpdateButtons()
    {
        if (currentSelection != null)
        {
            if (currentSelection.GetComponent<CharacterSelectButton>().characterName == "Empty")
            {
                btnCreateCharacter.interactable = true;
                btnSingleplayer.interactable = false;
                btnMultiplayer.interactable = false;
                btnDeleteCharacter.interactable = false;//setting this to false since it is not yet implemented
            }
            else
            {
                btnCreateCharacter.interactable = false;
                btnSingleplayer.interactable = true;
                btnMultiplayer.interactable = true;
                btnDeleteCharacter.interactable = true;//setting this to false since it is not yet implemented
            }
            SaveController.instance.currentSave = currentSelection.GetComponent<CharacterSelectButton>().saveNumber;
        }
        else
        {
            btnSingleplayer.interactable = false;
            btnMultiplayer.interactable = false;
            btnDeleteCharacter.interactable = false;
            btnCreateCharacter.interactable = false;
        }
    }
}