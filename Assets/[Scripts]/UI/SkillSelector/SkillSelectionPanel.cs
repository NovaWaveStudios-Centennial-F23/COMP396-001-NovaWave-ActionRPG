/**Created by Han Bi
 * Creates a panel that shows the player all the skills they have avaliable
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionPanel : MonoBehaviour
{
    [SerializeField]
    GameObject skillSelectionButton;

    [SerializeField]
    Transform skillSelectionContainer;

    List<GameObject> skillSelectionButtons = new();

    private void OnDisable()
    {
        CleanupContainer();
    }

    private void OnEnable()
    {
        GenerateSkillOptions();
    }

    private void GenerateSkillOptions()
    {
        //List<string> activeSkills = SkillTreeController.instance.GetSkills();

        //generating test data
        List<string> activeSkills = new List<string> { "Fireball", "Fireball", "Fireball", "Fireball" };

        foreach (var skill in activeSkills)
        {
            GameObject _obj = Instantiate(skillSelectionButton);
            _obj.transform.SetParent(skillSelectionContainer);
            _obj.GetComponent<SkillSelectionButton>().SetSkill(skill);
            _obj.transform.localScale = Vector3.one;
            skillSelectionButtons.Add(_obj);
        }
    }

    private void CleanupContainer()
    {
        foreach(var button in skillSelectionButtons)
        {
            Destroy(button);
        }
    }


}
