using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
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
        if (Input.GetKeyDown(KeyCode.Q) && !activeSkillCooldown.ContainsKey(nameof(Fireball)))
        {
            SkillCastProjectile(nameof(Fireball));
        }
        if (Input.GetKeyDown(KeyCode.W) && !activeSkillCooldown.ContainsKey(nameof(FrostNova)))
        {
            SkillCastPlayer(nameof(FrostNova));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillCastLocation(nameof(LightningStrike));
        }
    }

    private void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
        }
    }

    public void SkillCastProjectile(string skill)
    {
        activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
        CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

        GameObject activeSkill = Instantiate(activeSkillSO.prefab, projectileSpawner.transform.position, Quaternion.identity);
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;

        CalculationController.Instance.CalculateSkillDamage(activeSkillSO);

        activeSkillCooldown.Add(skill, activeSkillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue);
    }

    public void SkillCastPlayer(string skill)
    {
        activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
        CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

        GameObject activeSkill = Instantiate(activeSkillSO.prefab, player.transform.position, Quaternion.identity);
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;

        activeSkillCooldown.Add(skill, activeSkillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue);
    }

    public void SkillCastLocation(string skill)
    {
        activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
        CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

        GameObject activeSkill = Instantiate(activeSkillSO.prefab, mousePosition, Quaternion.identity);
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;

        activeSkillCooldown.Add(skill, activeSkillSO.allStats.Find(x => x.stat == Stats.Stat.Cooldown).minValue);
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
