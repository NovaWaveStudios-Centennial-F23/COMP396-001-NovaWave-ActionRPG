using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsController : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private FireballScriptableObject fireballSO;
    [SerializeField] private FrostNovaScriptableObject frostNovaSO;

    [Header("Prefabs")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject frostNovaPrefab;

    [Header("Others")]
    [SerializeField] private LayerMask groundMask;

    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //IGNITE
        //Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
        }

        //Instantiate Ignite Prefab
        if (Input.GetKeyDown(KeyCode.E))
        {
            InitFireball();
        }

        //PLASMA FIELD
        if (Input.GetKeyDown(KeyCode.F))
        {
            InitFrostNova();
        }
    }

    private void InitFireball()
    {
        fireballSO= Resources.Load<FireballScriptableObject>("Skills/Fireball/Fireball2");
        GameObject ignite = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        ignite.GetComponent<Fireball>().fireball = fireballSO;
        ignite.GetComponent<Fireball>().direction = mousePosition;
    }

    private void InitFrostNova()
    {
        frostNovaSO = Resources.Load<FrostNovaScriptableObject>("Skills/PlasmaField/PlasmaField1");
        GameObject plasmaField = Instantiate(frostNovaPrefab, transform.position, Quaternion.identity);
        plasmaField.GetComponent<FrostNova>().frostNova = frostNovaSO;
    }
}
