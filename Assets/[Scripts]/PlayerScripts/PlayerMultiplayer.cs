using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Stats;

public class PlayerMultiplayer : NetworkBehaviour
{
    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject projectileSpawner;

    private void Start()
    {
        if (isLocalPlayer)
        {
            //allow the camera to be created
            playerCamera.SetActive(true);

            //register self with the skill controller
            SkillsController.Instance.Init(gameObject, projectileSpawner);

            //Setup health for player
            Health playerHealth = GetComponent<Health>();
            playerHealth.CmdSetupHealth(
                StatsController.Instance.GetPlayerModifier(Stat.Health).minValue,
                StatsController.Instance.GetPlayerModifier(Stat.Health).minValue);
            InGameUIManager.Instance.playerHealthBar.Show(playerHealth.lifepool);

            //setup mana for player
            Mana mana = GetComponent<Mana>();
            InGameUIManager.Instance.playerManaBar.Initialize(mana);
        }
        else
        {
            playerCamera.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

}
