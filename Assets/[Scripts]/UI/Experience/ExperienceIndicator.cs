/**Created by: Han Bi
 * Used to display experience in bar form
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceIndicator : MonoBehaviour
{
    [SerializeField]
    Image expBar;

    
    private void Start()
    {
        StartCoroutine(Initialize());
    }


    IEnumerator Initialize()
    {
    
        while (ExperienceManager.Instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        ExperienceManager.Instance.OnExpGain += HandleExpGain;
        HandleExpGain(ExperienceManager.Instance.CurrentExperience);
    }

    private void HandleExpGain(int currentExp)
    {
        float fillamount = ((float)currentExp / (float)ExperienceManager.Instance.ExperienceToNextLevel);
        expBar.fillAmount = fillamount;
    }

    private void OnDestroy()
    {
        //TODO: Unsubscribe from exp gain event
        ExperienceManager.Instance.OnExpGain -= HandleExpGain;
    }




}
