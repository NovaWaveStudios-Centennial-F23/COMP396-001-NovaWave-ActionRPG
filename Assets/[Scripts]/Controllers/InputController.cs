/**Created by Han Bi
 * Used to handle player inputs
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    public Dictionary<KeyCode, string> keySpellPair = new Dictionary<KeyCode, string>();
    private bool inGame;
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

    void HandleSceneChange(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "StartMenu" || scene.name == "CharacterSelectionMenu")
        {
           inGame = false;
        }
        else
        {
            inGame = true;
        }
    }
    

    private void Update()
    {
        if (inGame)
        {
            //check if player is trying to cast spell
            foreach (KeyCode key in keySpellPair.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    //Debug.Log($"{key} detected: attempting to cast {keySpellPair[key]}");
                    SkillsController.Instance.SkillCast(keySpellPair[key]);
                }
            }
        }

    }

    public void RegisterSpell(KeyCode key, string spellName)
    {
        if (keySpellPair.ContainsKey(key))
        {
            keySpellPair[key] = spellName;
        }
        else
        {
            keySpellPair.Add(key,spellName);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneChange;
    }




}
