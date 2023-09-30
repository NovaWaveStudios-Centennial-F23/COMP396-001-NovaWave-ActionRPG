using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsController : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private IgniteScriptableObject igniteSO;
    [SerializeField] private PlasmaFieldScriptableObject plasmaFieldSO;

    [Header("Prefabs")]
    [SerializeField] private GameObject ignitePrefab;
    [SerializeField] private GameObject plasmaFieldPrefab;

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
            InitIgnite();
        }

        //PLASMA FIELD
        if (Input.GetKeyDown(KeyCode.F))
        {
            InitPlasmaField();
        }
    }

    private void InitIgnite()
    {
        igniteSO= Resources.Load<IgniteScriptableObject>("Skills/Ignite/Ignite2");
        GameObject ignite = Instantiate(ignitePrefab, transform.position, Quaternion.identity);
        ignite.GetComponent<Ignite>().ignite = igniteSO;
        ignite.GetComponent<Ignite>().direction = mousePosition;
    }

    private void InitPlasmaField()
    {
        plasmaFieldSO = Resources.Load<PlasmaFieldScriptableObject>("Skills/PlasmaField/PlasmaField1");
        GameObject plasmaField = Instantiate(plasmaFieldPrefab, transform.position, Quaternion.identity);
        plasmaField.GetComponent<PlasmaField>().plasmaField = plasmaFieldSO;
    }
}
