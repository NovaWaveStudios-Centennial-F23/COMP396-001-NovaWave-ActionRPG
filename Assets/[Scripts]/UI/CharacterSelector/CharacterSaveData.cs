/**Created by: Han Bi
 * Used to display data from save file
 * Originally used to test Character selector
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSaveData
{ 
    public string Name;
    public int Level;
    public Sprite Icon;

    public CharacterSaveData(string name, int level)
    {
        Name = name;
        Level = level;
    }
}
