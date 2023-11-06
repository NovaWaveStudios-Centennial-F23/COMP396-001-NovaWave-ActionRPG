/** Created by: Han Bi
 *  Used to display player's current level
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtLevel;

    void Start()
    {
        //try to get the player controller
        StartCoroutine(UpdatePlayerLevel());

    }

    IEnumerator UpdatePlayerLevel()
    {
        //wait until player controller is avaliable
        while (PlayerController.Instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        txtLevel.text = PlayerController.Instance.CurrentLevel.ToString();
        PlayerController.Instance.OnLevelChange += HandleLevelChange;
    }

    private void HandleLevelChange(int level)
    {
        txtLevel.text = level.ToString();
    }

    private void OnDestroy()
    {
        PlayerController.Instance.OnLevelChange -= HandleLevelChange;
    }
}
