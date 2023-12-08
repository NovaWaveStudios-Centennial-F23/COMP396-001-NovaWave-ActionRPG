using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController instance;
    [HideInInspector] public SaveData currentData;
    public List<int> skillTreeData;
    public CharacterSelector characterSelector;
    public int currentSave;
    public string characterName;
    public int characterLevel;
    public int characterExperience;
    public int experienceRequired;
    public int skillPoints;

    int saveNumber = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameUIManager.Instance.ToggleSkillTree();
        LoadSave("Save 1");
        LoadSave("Save 2");
        LoadSave("Save 3");
        LoadSave("Save 4");
        InGameUIManager.Instance.ToggleSkillTree();
    }

    public void OnSave()
    {
        this.currentData.playerData.characterName = characterName;
        this.currentData.playerData.skillTreeData = SkillTreeController.instance.GetSkillTreeNodeLevels();
        this.currentData.playerData.characterLevel = ExperienceManager.Instance.CurrentLevel;
        this.currentData.playerData.characterExperience = ExperienceManager.Instance.CurrentExperience;
        this.currentData.playerData.experienceRequired = ExperienceManager.Instance.ExperienceToNextLevel;
        this.currentData.playerData.skillPoints = PlayerController.Instance.SkillSkillPoints;

        SerializationController.Save("Save " + currentSave, this.currentData);
    }

    public void OnLoad()
    {
        SaveData.currentData = (SaveData)SerializationController.Load(Application.persistentDataPath + "/mysticrealms/Save " + currentSave + ".xml");

        characterName = SaveData.currentData.playerData.characterName;
        skillTreeData = SaveData.currentData.playerData.skillTreeData;
        characterLevel = SaveData.currentData.playerData.characterLevel;
        characterExperience = SaveData.currentData.playerData.characterExperience;
        experienceRequired = SaveData.currentData.playerData.experienceRequired;

        SkillTreeController.instance.LoadSkillTree(skillTreeData);
        ExperienceManager.Instance.CurrentLevel = characterLevel;
        ExperienceManager.Instance.CurrentExperience = characterExperience;
        ExperienceManager.Instance.ExperienceToNextLevel = experienceRequired;
        PlayerController.Instance.CurrentLevel = characterLevel;
        PlayerController.Instance.SkillSkillPoints = skillPoints;
    }

    public void LoadSave(string saveName)
    {
        object data = SerializationController.Load(Application.persistentDataPath + "/mysticrealms/" + saveName + ".xml");        

        if (data != null)
        {
            SaveData.currentData = (SaveData)data;
            characterSelector.GenerateData(SaveData.currentData.playerData.characterName, SaveData.currentData.playerData.characterLevel, saveNumber);
        }
        else
        {
            characterSelector.GenerateData("Empty", 0, saveNumber);
        }

        saveNumber++;
    }

    public void DeleteSave()
    {
        SerializationController.Delete(Application.persistentDataPath + "/mysticrealms/Save " + currentSave + ".xml");

        characterSelector.currentSelection.GetComponent<CharacterSelectButton>().UpdateName("Empty");
        characterSelector.currentSelection.GetComponent<CharacterSelectButton>().UpdateLevel(0);
    }
}
