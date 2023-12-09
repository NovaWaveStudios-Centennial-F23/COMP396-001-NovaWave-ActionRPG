using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _currentData;

    public static SaveData currentData
    {
        get
        {
            if (_currentData == null)
            {
                _currentData = new SaveData();
            }
            return _currentData;
        }
        set
        {
            if (value != null)
            {
                _currentData = value;
            }
        }
    }

    public PlayerData playerData;
}

[System.Serializable]
public class PlayerData
{
    public List<int> skillTreeData; 
    public string characterName;
    public int skillPoints;
    public int characterLevel;
    public int characterExperience;
    public int experienceRequired;
}
