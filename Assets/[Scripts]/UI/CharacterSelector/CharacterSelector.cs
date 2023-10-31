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
    

    GameObject currentSelection;


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

        if (generateFakeData)
        {
            GenerateDummyData();
        }


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
    private void GenerateDummyData()
    {
        CharacterSaveData s1 = new CharacterSaveData("Desuburingaa1", 10);
        var obj1 = Instantiate(characterOptionPrefab);
        obj1.transform.parent = transform;
        obj1.GetComponent<CharacterSelectButton>().PopulateData(s1);
        obj1.GetComponent<CharacterSelectButton>();
        characterSelectionHandler.AddButton(obj1.GetComponent<CharacterSelectButton>());
        characterCount++;

        CharacterSaveData s2 = new CharacterSaveData("xxNoobSlayer69", 28);
        var obj2 = Instantiate(characterOptionPrefab);
        obj2.transform.parent = transform;
        obj2.GetComponent<CharacterSelectButton>().PopulateData(s2);
        characterSelectionHandler.AddButton(obj2.GetComponent<CharacterSelectButton>());
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
        if(currentSelection != null)
        {
            btnSingleplayer.interactable = true;
            btnMultiplayer.interactable = true;
            btnDeleteCharacter.interactable = true;
        }
        else
        {
            btnSingleplayer.interactable = false;
            btnMultiplayer.interactable = false;
            btnDeleteCharacter.interactable = false;
        }
    }








}
