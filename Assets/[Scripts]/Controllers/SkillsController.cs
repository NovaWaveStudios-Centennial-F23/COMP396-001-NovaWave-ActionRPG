using System;
using System.Collections.Generic;
using UnityEngine;
using static Stats;
using static StatFinder;

public class SkillsController : MonoBehaviour
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
        Raycast();
    }

    private void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
            if (projectileSpawner == null)
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }
                projectileSpawner = player.GetComponentInChildren<ProjectileSpawner>().gameObject;
            }
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
                    if(projectileSpawner == null)
                    {
                        if(player == null)
                        {
                            player = GameObject.FindGameObjectWithTag("Player");
                        }
                        projectileSpawner = player.GetComponentInChildren<ProjectileSpawner>().gameObject;
                    }
                    spawnLocation = projectileSpawner.transform.position;
                    break;
                case SkillSO.SkillType.OnPlayer:
                    if(player == null)
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

            // Instantiate skill
            GameObject activeSkill = Instantiate(activeSkillSO.prefab, spawnLocation, Quaternion.identity);
            activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;

            // Activate Skill Cooldown
            activeSkillCooldown.Add(skill, FindStat(activeSkillSO, Stat.Cooldown).minValue);
            if(playerMana == null)
            {
                playerMana = player.GetComponent<Mana>();
            }
            playerMana.SpendMana(FindStat(activeSkillSO, Stat.ManaCost).minValue);
        }
        else
        {
            Debug.Log(skill + " is on cooldown.");
        }
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
}
