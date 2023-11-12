using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealthUI : MonoBehaviour
{
    Health character;

    private void Awake()
    {
        character = GetComponent<Health>();
        SceneManager.sceneLoaded += HandleSceneChange;
    }

    [SerializeField] UIPoolBar hpBar;

    void HandleSceneChange(Scene currScene, LoadSceneMode mode)
    {

        if (currScene.name == "StartMenu" || currScene.name == "CharacterSelectionMenu")
        {
            
        }
        else
        {
            hpBar = InGameUIManager.Instance.playerHealthBar;
            character = GetComponent<Health>();
        }
    }


    private void Update()
    {
       
        if(hpBar == null) {
            hpBar = InGameUIManager.Instance.playerHealthBar;
            
        }
        if (character == null)
        {
            character = GetComponent<Health>();
        }

        hpBar.Show(character.lifepool);
    }

}

