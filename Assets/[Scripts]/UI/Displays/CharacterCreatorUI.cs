/*** Created by Han Bi
 * Used to display UI information for the Character Creator
 * Requires a class selection handler to observe changes from
 */

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreatorUI : MonoBehaviour
{
    [SerializeField]
    GameObject instructions;

    [SerializeField]
    GameObject className;

    [SerializeField]
    GameObject classDescription;

    [SerializeField]
    Button finishButton;

    [SerializeField]
    Button cancelButton;

    [SerializeField]
    TMP_InputField characterNameInput;

    [SerializeField]
    ClassSelectionHandler classSelector;

    [SerializeField]
    TextMeshProUGUI comingSoonText;

    TextMeshProUGUI classNameText;
    TextMeshProUGUI classDescriptionText;

    private void Awake()
    {
        if(classSelector != null)
        {
            classSelector.OnCharacterSelected += ShowClassInfo;
        }
        
    }

    private void Start()
    {
        classNameText = className.GetComponentInChildren<TextMeshProUGUI>();
        classDescriptionText = classDescription.GetComponentInChildren<TextMeshProUGUI>();
        cancelButton.gameObject.SetActive(true);
        HideCharacterInfo();
    }


    private void ShowClassInfo(ClassDescriptionSO data)
    {
        instructions.gameObject.SetActive(false);

        classNameText.text = data.className;
        className.SetActive(true);
        classDescriptionText.text = data.classDescription;
        classDescription.SetActive(true);

        if(data.isAvaliable)
        {
            finishButton.gameObject.SetActive(true);
            characterNameInput.gameObject.SetActive(true);
            comingSoonText.gameObject.SetActive(false);
        }
        else
        {
            finishButton.gameObject.SetActive(false);
            characterNameInput.gameObject.SetActive(false);
            comingSoonText.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if(classSelector != null)
        {
            classSelector.OnCharacterSelected -= ShowClassInfo;
        }
    }

    private void OnDisable()
    {
        HideCharacterInfo();
    }

    private void HideCharacterInfo()
    {
        instructions.gameObject.SetActive(true);

        classDescription.SetActive(false);
        className.SetActive(false);
        finishButton.gameObject.SetActive(false);
        characterNameInput.text = "";
        characterNameInput.gameObject.SetActive(false);
        comingSoonText.gameObject.SetActive(false);


    }

    public void CreateCharacter()
    {
        if(SkillTreeController.instance != null)
        {
            SkillTreeController.instance.LoadSkillTree(new List<int>());
        }

        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.ResetPlayerInfo();
        }

        SceneManager.LoadScene("MainLevel");
    }

}
