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
        //StartCoroutine(Initialize());
    }


    //IEnumerator Initialize()
    //{
    //    TODO: Reference experience manager
    //    while (___ == null)
    //    {
            //yield return new WaitForSeconds(0.1f);
    //    }
    //    

    //    TODO: Subscribe to exp gain event

    //}

    private void HandleExpGain(float currentExp, float maxExp)
    {
        float fillamount = currentExp / maxExp;

        expBar.fillAmount = fillamount;
    }

    private void OnDestroy()
    {
        //TODO: Unsubscribe from exp gain event
    }




}
