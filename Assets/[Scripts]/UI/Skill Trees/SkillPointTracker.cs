using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPointTracker : MonoBehaviour
{
    [SerializeField]
    bool isPlayerSkillTree;

    [SerializeField]
    TextMeshProUGUI txtSkillPointsDisplay;

    private void Start()
    {
        StartCoroutine(GetPlayerControllerInstance());
    }

    void OnEnable()
    {
        // Safely check if PlayerController.Instance and its properties are not null
        if (PlayerController.Instance != null)
        {
            if (isPlayerSkillTree && PlayerController.Instance.PlayerSkillPoints != null)
            {
                HandleSkillChange(PlayerController.Instance.PlayerSkillPoints);
            }
            else if (PlayerController.Instance.SkillSkillPoints != null)
            {
                HandleSkillChange(PlayerController.Instance.SkillSkillPoints);
            }
        }
        else
        {
            Debug.LogWarning("PlayerController.Instance is not initialized yet.");
        }
    }


    IEnumerator GetPlayerControllerInstance()
    {
        float timeElapsed = 0;
        //wait until we can access a player instance or after 5seconds
        while(PlayerController.Instance == null || timeElapsed > 5f)
        {
            timeElapsed+=Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }

        try
        { 
            //subscribe to skill point change events and get inital value
            if (isPlayerSkillTree)
            {
                txtSkillPointsDisplay.text = PlayerController.Instance.PlayerSkillPoints.ToString();
                PlayerController.Instance.OnPlayerSkillPointsChange += HandleSkillChange;
            }
            else
            {
                txtSkillPointsDisplay.text = PlayerController.Instance.SkillSkillPoints.ToString();
                PlayerController.Instance.OnSkillSkillPointsChange += HandleSkillChange;
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError($"Error: {e}");
        }
    }

    private void HandleSkillChange(int skillamount)
    {
        txtSkillPointsDisplay.text = skillamount.ToString();
    }

    private void OnDestroy()
    {
        try
        {
            if (isPlayerSkillTree)
            {
                PlayerController.Instance.OnPlayerSkillPointsChange -= HandleSkillChange;
            }
            else
            {
                PlayerController.Instance.OnSkillSkillPointsChange -= HandleSkillChange;
            }
        }
        catch
        {

        }
    }

}
