/**Created by: Han Bi
 * Used to teleport player to town when clicked
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TownTeleporter : MonoBehaviour
{
    [SerializeField]
    SceneController controller;

    [SerializeField]
    GameObject teleportPanel;

    private void Start()
    {
        ClosePanel();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerMultiplayer>().isClientOnly)
            {
                teleportPanel.SetActive(true);
            }
        }
    }

    public void ClosePanel()
    {
        teleportPanel.SetActive(false);
    }

    public void Teleport()
    {
        ClosePanel();
        controller.LoadTown();
    }
}
