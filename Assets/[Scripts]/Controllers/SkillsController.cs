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

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;

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

    void Update()
    {
        Raycast();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillCast(nameof(Fireball));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SkillCast(nameof(FrostNova));
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

    public void SkillCast(string skill)
    {
        activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");
        CalculationController.Instance.CalculateSkillStats(skill, activeSkillSO);

        GameObject activeSkill = Instantiate(activeSkillSO.prefab, player.transform.position, Quaternion.identity);
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;
    }
}
