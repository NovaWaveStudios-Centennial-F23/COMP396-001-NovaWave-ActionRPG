using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A placeholder ValuePool class 
public class ValuePool
{
    public float maxValue;
    public float currentValue;
}

public class UIPoolBar : MonoBehaviour
{
    [SerializeField] Image bar;

    ValuePool targetPool;


    public void ShowPool(ValuePool targetPool)
    {
        this.targetPool = targetPool;
    }

    public void Clear()
    {
        this.targetPool = null;
    }

    private void Update()
    {
        if (targetPool != null)
        {
            bar.fillAmount = Mathf.InverseLerp(0f, targetPool.maxValue, targetPool.currentValue);
        }
    }
}
