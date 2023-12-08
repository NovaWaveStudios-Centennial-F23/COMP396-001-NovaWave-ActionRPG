using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController instance;
    [HideInInspector] public SaveData currentData; 
    public List<GearSO> equippedGear;
    public List<int> skillTreeData;
    public CharacterSelector characterSelector;
    public int currentSave;
    public string characterName;
    public int characterLevel;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSave()
    {
        this.currentData.playerData.characterName = characterName;
        this.currentData.playerData.characterLevel = characterLevel;
        this.currentData.playerData.skillTreeData = SkillTreeController.instance.GetSkillTreeNodeLevels();
        this.currentData.playerData.equippedGear = equippedGear;

        SerializationController.Save("Save " + currentSave, this.currentData);
    }

    public void OnLoad()
    {
        SaveData.currentData = (SaveData)SerializationController.Load(Application.persistentDataPath + "/mysticrealms/Save " + currentSave + ".xml");

        characterName = SaveData.currentData.playerData.characterName;
        characterLevel = SaveData.currentData.playerData.characterLevel;
        skillTreeData = SaveData.currentData.playerData.skillTreeData;
        equippedGear = SaveData.currentData.playerData.equippedGear;

        SkillTreeController.instance.LoadSkillTree(skillTreeData);
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
}
