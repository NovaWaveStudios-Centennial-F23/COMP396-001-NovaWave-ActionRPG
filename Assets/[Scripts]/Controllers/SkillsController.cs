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
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> fireballPrefab;
    [SerializeField] private GameObject frostNovaPrefab;

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;

    private Vector3 mousePosition;

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

    void Update()
    {
        SkillCast(nameof(Fireball));
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

    public void SkillCast(string skill)
    {
        //FIREBALL
        Raycast();
        if (Input.GetKeyDown(KeyCode.E) && !fireballCooldown)
        {
            InitActiveSkill(skill);
        }
    }

    private void InitActiveSkill(string skill)
    {
        activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
        CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

        GameObject fireball = Instantiate(activeSkillSO.prefab, player.transform.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().fireball = activeSkillSO;
        fireball.GetComponent<Fireball>().direction = mousePosition;
        fireballCooldown = true;
    }
}
