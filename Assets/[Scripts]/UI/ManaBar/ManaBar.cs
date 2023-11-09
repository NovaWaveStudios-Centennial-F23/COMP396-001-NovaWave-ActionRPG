/**Created by: Han Bi
 * Bar that represents 
 */
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    Image manaBar;

    Object mana;

    void Start()
    {
        mana = FindFirstObjectByType(typeof(Mana));
        if(mana == null)
        {
            Debug.Log("Could not Mana Script.");
        }
        else
        {
            mana.GetComponent<Mana>().PoolChanged += HandleValueChanged;
        }
       
    }

    private void HandleValueChanged(ValuePool pool)
    {
        manaBar.fillAmount = pool.currentValue / pool.maxValue;
    }

    void OnDestroy()
    {
        if (mana != null)
        {
            mana.GetComponent<Mana>().PoolChanged -= HandleValueChanged;
        }
    }

}