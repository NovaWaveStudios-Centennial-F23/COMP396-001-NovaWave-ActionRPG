using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthUI : MonoBehaviour
{
    Health character;

    private void Awake()
    {
        character = GetComponent<Health>();

    }

    [SerializeField] UIPoolBar hpBar;

    private void Update()
    {
        hpBar.Show(character.lifepool);
    }

}

