using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsController : MonoBehaviour
{
    private static SkillsController instance;
    public static SkillsController Instance { get { return instance; } }

    public bool fireballCooldown = false;

    [Header("Scriptable Objects")]
    [SerializeField] private SkillSO fireballSO;

    [Header("Prefabs")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject frostNovaPrefab;

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SkillTreeManager fireballSkillTree;

    private Vector3 mousePosition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        SkillCast();
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

    private void SkillCast()
    {
        //FIREBALL
        Raycast();
        if (Input.GetKeyDown(KeyCode.E) && !fireballCooldown)
        {
            InitFireball();
        }
    }

    private void InitFireball()
    {
        fireballSO = Resources.Load<SkillSO>("Skills/Fireball/Fireball" + fireballSkillTree.fireballLvl.ToString());

        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().fireball = fireballSO;
        fireball.GetComponent<Fireball>().direction = mousePosition;

        if (fireballSkillTree.GetSkillTree()[5].statValue == 1)
        {
            GameObject fireball2 = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball2.GetComponent<Fireball>().fireball = fireballSO;
            fireball2.GetComponent<Fireball>().direction = mousePosition;
        }
        fireballCooldown = true;
    }
}
