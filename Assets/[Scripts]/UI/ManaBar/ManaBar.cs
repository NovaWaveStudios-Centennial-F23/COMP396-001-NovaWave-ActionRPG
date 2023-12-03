/**Created by: Han Bi
 * Bar that represents 
 */
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    Image manaBar;

    [SerializeField]
    TextMeshProUGUI manaIndicator;
        
    Object mana;


    //void Start()
    //{
    //    mana = FindFirstObjectByType(typeof(Mana));
    //    if(mana == null)
    //    {
    //        Debug.Log("Could not find Mana Script.");
    //    }
    //    else
    //    {
    //        mana.GetComponent<Mana>().PoolChanged += HandleValueChanged;
    //    }
    //    SceneManager.sceneLoaded += HandleSceneChange;
    //}

    //void HandleSceneChange(Scene currScene, LoadSceneMode mode)
    //{

    //    if (currScene.name == "StartMenu" || currScene.name == "CharacterSelectionMenu")
    //    {

    //    }
    //    else
    //    {
    //        mana = FindFirstObjectByType(typeof(Mana));
    //        if (mana == null)
    //        {
    //            Debug.Log("Could not Mana Script.");
    //        }
    //        else
    //        {
    //            mana.GetComponent<Mana>().PoolChanged += HandleValueChanged;
    //        }
    //    }
    //}

    public void Initialize(Mana mana)
    {
        this.mana = mana;
        mana.GetComponent<Mana>().PoolChanged += HandleValueChanged;
        HandleValueChanged(mana.manaPool);
    }


    private void HandleValueChanged(ValuePool pool)
    {
        manaBar.fillAmount = pool.currentValue / pool.maxValue;
        manaIndicator.text = Mathf.Floor(pool.currentValue).ToString();
    }

    void OnDestroy()
    {
        if (mana != null)
        {
            mana.GetComponent<Mana>().PoolChanged -= HandleValueChanged;
        }
    }

}
