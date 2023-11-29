/**Created by: Han Bi
 * Used to manage all skill slots
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelectionManager : MonoBehaviour
{
    private SkillSlot[] allSlots;

    private Queue<SkillSlot> emptySlotQueue = new Queue<SkillSlot>();
    private Dictionary<string, bool> skillsSelected = new Dictionary<string, bool>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        allSlots = GetComponentsInChildren<SkillSlot>();
        emptySlotQueue.Clear();
        skillsSelected.Clear();
        StartCoroutine(nameof(WaitForSkillTreeController));
    }

    IEnumerator WaitForSkillTreeController()
    {
        while(SkillTreeController.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }
        SkillTreeController.instance.OnSkillTreeChanged += HandleSkillTreeChange;
        HandleSkillTreeChange();


    }

    private void RefreshData()
    {
        allSlots = GetComponentsInChildren<SkillSlot>();
        emptySlotQueue.Clear();
        skillsSelected.Clear();

        //check if slot is empty or not
        foreach (SkillSlot slot in allSlots)
        {
            string skillName = slot.GetSelectedSkill();
            if (skillName == null || skillName == "")
            {
                emptySlotQueue.Enqueue(slot);
            }
            else
            {
                if (!skillsSelected.ContainsKey(skillName))
                {
                    skillsSelected.Add(skillName, true);
                }
                else
                {
                    skillsSelected[skillName] = true;
                }
            }
        }
    }

    private void HandleSkillTreeChange()
    {
        allSlots = GetComponentsInChildren<SkillSlot>();
        List<string> activeSkills = SkillTreeController.instance.GetSkills();
        
        //reset any slots that is binded to a spell that is no longer avaliable
        foreach (SkillSlot slot in allSlots)
        {
            bool skillSelected = false;

            foreach (string skill in activeSkills)
            {
                if(slot.GetSelectedSkill() == skill)
                {
                    skillSelected = true;
                    break;
                }
            }

            if (!skillSelected)
            {
                slot.Reset();
            }
        }

        RefreshData();

        foreach (string skill in activeSkills)
        {
            bool skillSelected = false;

            if (skillsSelected.ContainsKey(skill))
            {
                skillSelected = skillsSelected[skill];
            }

            if (!skillSelected && emptySlotQueue.Count > 0)
            {
                //automatically put the skill into the first avaliable skillslot
                SkillSlot emptySlot = emptySlotQueue.Dequeue();
                emptySlot.SetSkill(skill);
            }
        }

    }


    private void OnDestroy()
    {
        SkillTreeController.instance.OnSkillTreeChanged -= HandleSkillTreeChange;
    }







}
