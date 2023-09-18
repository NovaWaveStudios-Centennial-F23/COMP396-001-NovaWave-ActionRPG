using UnityEngine;
using Mirror;

public class NetworkPlayerScript1 : NetworkBehaviour
{
    public TextMesh playerNameText;
    public GameObject floatingInfo;

    private Material playerMaterialClone;

    private SceneScript sceneScript;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColourChanged))]
    public Color playerColor = Color.white;

    private void Awake()
    {
        sceneScript = GameObject.FindObjectOfType<SceneScript>();
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
        {
            sceneScript.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
        }
    }

    void OnNameChanged(string _old, string _new)
    {
        playerNameText.text = playerName;
    }

    void OnColourChanged(Color _old, Color _new)
    {
        playerNameText.color = _new;
        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.color = _new;
        GetComponent<Renderer>().material = playerMaterialClone;

    }

    public override void OnStartLocalPlayer()
    {
        sceneScript.playerScript = this;

        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0,0,0);

        floatingInfo.transform.localPosition = new Vector3(0, 1.33f, 0);
        floatingInfo.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color);
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        playerName = _name;
        playerColor = _col;
        sceneScript.statusText = $"{playerName} joined.";
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }



}
