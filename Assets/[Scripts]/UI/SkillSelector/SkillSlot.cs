/**Created by Han Bi
 * UI object to allow player to select what spell to cast on which key
 * this component will pass the link between skill and key to another component to handle
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    [Tooltip("The skill will activate using this key")]
    KeyCode activationKey;

    [SerializeField]
    [Tooltip("The text that will display the key to player")]
    TextMeshProUGUI txtKeyText;

    //references the icon image component
    [SerializeField]
    Image iconImage;

    //this is a reference of the skill that will be activated when key is pressed
    string selectedSkill;

    //broadcasts an event letting listener know that keys have changed
    public event Action<Dictionary<KeyCode, string>> SkillKeyChanged = delegate { };

    public void OnPointerClick(PointerEventData eventData)
    {
        //show an option of all skills the player has unlocked
    }

    private void Start()
    {
        txtKeyText.text = activationKey.ToString();
        //testing
        
    }

    public void SetSkill(string skill)
    {
        //guards?
        selectedSkill = skill;

        //change the icon image
        iconImage.sprite = ActiveSkillUIData.Instance.GetSprite(typeof(Fireball).ToString());

        //let the input manager know

    }


}
