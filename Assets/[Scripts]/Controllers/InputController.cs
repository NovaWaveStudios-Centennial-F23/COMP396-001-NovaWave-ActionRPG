/**Created by Han Bi
 * Used to handle player inputs
 */

using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    public Dictionary<KeyCode, string> keySpellPair = new Dictionary<KeyCode, string>();
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //check if player is trying to cast spell
        foreach(KeyCode key in keySpellPair.Keys)
        {
            if(Input.GetKeyDown(key))
            {
                //Debug.Log($"{key} detected: attempting to cast {keySpellPair[key]}");
                SkillsController.Instance.SkillCastProjectile(keySpellPair[key]);
            }
        }
    }

    public void RegisterSpell(KeyCode key, string spellName)
    {
        //Debug.Log($"registering: {key} with {spellName}");
        if (keySpellPair.ContainsKey(key))
        {
            keySpellPair[key] = spellName;
        }
        else
        {
            keySpellPair.Add(key,spellName);
        }
    }







}
