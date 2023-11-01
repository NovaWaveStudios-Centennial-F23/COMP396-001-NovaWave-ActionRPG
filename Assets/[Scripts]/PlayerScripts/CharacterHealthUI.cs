using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthUI : MonoBehaviour
{
    CharacterDamage character;

    private void Awake()
    {
        character = GetComponent<CharacterDamage>();

    }

    [SerializeField] UIPoolBar hpBar;

    private void Update()
    {
        hpBar.Show(character.lifepool);
    }

}

