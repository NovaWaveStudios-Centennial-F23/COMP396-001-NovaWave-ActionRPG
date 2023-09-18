using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : NetworkBehaviour
{
    public TextMeshProUGUI canvasStatusText;
    public NetworkPlayerScript1 playerScript;

    [SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;

    void OnStatusTextChanged(string _old, string _new)
    {
        canvasStatusText.text = statusText;
    }

    public void ButtonSendMessage()
    {
        if(playerScript != null)
        {
            playerScript.CmdSendPlayerMessage();
        }
    }


}
