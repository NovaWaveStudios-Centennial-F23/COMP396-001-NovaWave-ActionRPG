using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsController : MonoBehaviour
{
    private static SkillsController instance;
    public static SkillsController Instance { get { return instance; } }

    [Header("Scriptable Objects")]
    [SerializeField] private FireballScriptableObject fireballSO;
    [SerializeField] private FrostNovaScriptableObject frostNovaSO;

    [Header("Prefabs")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject frostNovaPrefab;

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SkillTree skillTree;

    private Vector3 mousePosition;
    public bool fireballCooldown = false;

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

    private void SkillCast()
    {
        //FIREBALL
        //Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
        }

        //Instantiate Ignite Prefab
        if (Input.GetKeyDown(KeyCode.E) && !fireballCooldown)
        {
            InitFireball();
        }

        //FROST NOVA
        if (Input.GetKeyDown(KeyCode.F))
        {
            InitFrostNova();
        }
    }

    private void InitFireball()
    {
        fireballSO= Resources.Load<FireballScriptableObject>("Skills/Fireball/Fireball" + skillTree.fireballLvl.ToString());        
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().fireball = fireballSO;
        fireball.GetComponent<Fireball>().direction = mousePosition;
        fireballCooldown = true;
    }

    private void InitFrostNova()
    {
        frostNovaSO = Resources.Load<FrostNovaScriptableObject>("Skills/PlasmaField/PlasmaField1");
        GameObject frostNova = Instantiate(frostNovaPrefab, transform.position, Quaternion.identity);
        frostNova.GetComponent<FrostNova>().frostNova = frostNovaSO;
    }
}
