//Author: Mithul Koshy
using System;
using TMPro;
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
    [SerializeField] TextMeshProUGUI healthIndicator;

    ValuePool targetPool;

    public void Show(ValuePool targetPool)
    {
        this.targetPool = targetPool;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        this.targetPool = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (targetPool != null)
        {
            bar.fillAmount = Mathf.InverseLerp(0f, targetPool.maxValue, targetPool.currentValue);
            healthIndicator.text = targetPool.currentValue.ToString();
        }
    }

}
