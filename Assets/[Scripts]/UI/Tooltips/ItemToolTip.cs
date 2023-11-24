/* Created by: Yusuke Kuroki
 * Used to display details for Items
 * It's made from SkillToolTip.cs by Han Bi
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField]
    Image iconImage;

    [SerializeField]
    TextMeshProUGUI txtItemName;

    [SerializeField]
    TextMeshProUGUI txtItemDescription;

    private List<TextMeshProUGUI> optionalTooltipElements;

    private void Awake()
    {
        optionalTooltipElements = new List<TextMeshProUGUI>()
        {
            txtItemName,
            txtItemDescription,
        };
    }

    public void DisplayDetails(ItemSO item)
    {
        DisplayIcon(item.icon);
        DisplayName(item.data.Name);
        DisplayDescription(item.description);

        //turn off all optional elements
        foreach(var t in optionalTooltipElements)
        {
            t.gameObject.SetActive(false);
        }
    }
    private void DisplayIcon(Sprite image)
    {
        iconImage.sprite = image;
    }

    private void DisplayName(string name)
    {
        txtItemName.text = name;
    }

    private void DisplayDescription(string description)
    {
        txtItemDescription.text = description;
    }
}
