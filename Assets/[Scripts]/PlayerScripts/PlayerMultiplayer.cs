using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Stats;

public class PlayerMultiplayer : NetworkBehaviour
{
    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject projectileSpawner;

    [SerializeField]
    Health health;

    [SerializeField]
    Mana mana;

    private void Start()
    {
        StartCoroutine(nameof(Initialize));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    IEnumerator Initialize()
    {
        if (isLocalPlayer)
        {
            
            //allow the camera to be created
            playerCamera.SetActive(true);

            //register self with the skill controller
            SkillsController.Instance.Init(gameObject, projectileSpawner);

            while(StatsController.Instance == null || InGameUIManager.Instance == null)
            {
                
                yield return new WaitForEndOfFrame();

            }

            Debug.Log($"Stats intialization for {netId} started.");
            //Setup health for player
            health.CmdSetupHealth(
            StatsController.Instance.GetPlayerModifier(Stat.Health).minValue,
            StatsController.Instance.GetPlayerModifier(Stat.HealthRegen).minValue);
            InGameUIManager.Instance.playerHealthBar.Show(health.lifepool);

            //setup mana for player
            InGameUIManager.Instance.playerManaBar.Initialize(mana);

        }
        else
        {
            playerCamera.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

}
