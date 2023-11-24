using System;
using System.Collections.Generic;
using UnityEngine;
using static Stats;
using static StatFinder;
using Mirror;

public class SkillsController : NetworkBehaviour
{
    private static SkillsController instance;
    public static SkillsController Instance { get { return instance; } }

    public bool fireballCooldown = false;

    [Header("Scriptable Objects")]
    [SerializeField] private ActiveSkillSO activeSkillSO;

    [Header("Prefabs")]
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject projectileSpawner;

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;

    private Dictionary<string, float> activeSkillCooldown;
    public Vector3 mousePosition;
    private Mana playerMana;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        activeSkillCooldown = new Dictionary<string, float>();
    }

    void Update()
    {
        //why do we need this in update?
        Raycast();
    }

    private void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
            mousePosition.y = projectileSpawner.transform.position.y;
        }
    }

    public void SkillCast(string skill)
    {
        if (!activeSkillCooldown.ContainsKey(skill))
        {
            // Set Skill Stats (Subject to change)
            activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
            CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

            // Set Spawn Location
            Vector3 spawnLocation = new Vector3(0, 0, 0);
            switch (activeSkillSO.skillType)
            {
                case SkillSO.SkillType.Projectile:
                    if (projectileSpawner == null)
                    {
                        if (player == null)
                        {
                            player = GameObject.FindGameObjectWithTag("Player");
                        }
                        projectileSpawner = player.GetComponentInChildren<ProjectileSpawner>().gameObject;
                    }
                    spawnLocation = projectileSpawner.transform.position;
                    break;
                case SkillSO.SkillType.OnPlayer:
                    if (player == null)
                    {
                        player = GameObject.FindGameObjectWithTag("Player");
                    }
                    spawnLocation = player.transform.position;
                    break;
                case SkillSO.SkillType.OnMouse:
                    spawnLocation = mousePosition;
                    break;
                default:
                    Debug.Log("Please assign a skill type");
                    break;
            }
            if (playerMana == null)
            {
                playerMana = player.GetComponent<Mana>();
            }

            if (playerMana.SpendMana(FindStat(activeSkillSO, Stat.ManaCost).minValue))
            {
                // Instantiate skill
                CmdCastSpell(spawnLocation);

                // Activate Skill Cooldown
                activeSkillCooldown.Add(skill, FindStat(activeSkillSO, Stat.Cooldown).minValue);
            }

        }
        else
        {
            Debug.Log(skill + " is on cooldown.");
        }
    }

    //not a player object, thus needs to set auth to false
    [Command(requiresAuthority = false)]
    private void CmdCastSpell(Vector3 spawnLocation)
    {
        GameObject activeSkill = Instantiate(activeSkillSO.prefab, spawnLocation, Quaternion.identity);
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;
        NetworkServer.Spawn(activeSkill);
    }

    public void SetSkillCooldown(string skill, float cooldown)
    {
        if (cooldown <= 0)
        {
            activeSkillCooldown.Remove(skill);
        }
        else
        {
            activeSkillCooldown[skill] = cooldown;
        }
    }

    public float GetSkillCooldown(string skill)
    {
        return activeSkillCooldown[skill];
    }

    /// <summary>
    /// Used in multiplayer to make sure skills controller is tracking the correct player object
    /// </summary>
    /// <param name="player"></param>
    /// <param name="projectileSpawner"></param>
    public void Init(GameObject player, GameObject projectileSpawner)
    {
        this.player = player;
        this.projectileSpawner = projectileSpawner;
    }
}
