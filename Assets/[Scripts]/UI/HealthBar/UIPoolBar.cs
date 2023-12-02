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
    public Image bar;
    public TextMeshProUGUI healthIndicator;

    ValuePool targetPool;

    public void Show(ValuePool targetPool)
    {
        this.targetPool = targetPool;
        UpdateUI(); // Update UI immediately on show
    }

    public void Clear()
    {
        this.targetPool = null;
        UpdateUI(); // Update UI immediately on clear
    }

    private void Update()
    {
        UpdateUI(); // Regular update of UI
    }

    private void UpdateUI()
    {
        if (targetPool != null)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true); // Enable the UI if it's not already enabled
            }

            bar.fillAmount = Mathf.InverseLerp(0f, targetPool.maxValue, targetPool.currentValue);
            healthIndicator.text = targetPool.currentValue.ToString("F0"); // Format for whole numbers, adjust as needed
        }
        else
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false); // Disable the UI if it's currently enabled
            }
        }
    }
}
