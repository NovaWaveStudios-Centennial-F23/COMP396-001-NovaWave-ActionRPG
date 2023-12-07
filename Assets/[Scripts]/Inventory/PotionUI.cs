using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionUI : MonoBehaviour
{
    public GameObject textAmountLifePotionUI;
    void Start()
    {
        textAmountLifePotionUI.GetComponent<TextMeshProUGUI>().text = ItemController.Instance.lifePotionCount.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        int lifePotionCount = ItemController.Instance.lifePotionCount;
        textAmountLifePotionUI.GetComponent<TextMeshProUGUI>().text = lifePotionCount.ToString();
    }
}
