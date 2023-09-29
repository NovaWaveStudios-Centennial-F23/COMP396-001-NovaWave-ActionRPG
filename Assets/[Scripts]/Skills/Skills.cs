using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skills : MonoBehaviour
{
    [SerializeField] private IgniteScriptableObject igniteScriptableObject;
    [SerializeField] private GameObject ignitePrefab;
    [SerializeField] private LayerMask groundMask;

    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            mousePosition = hit.point;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InitIgnite();
        }
    }

    private void InitIgnite()
    {
        igniteScriptableObject = Resources.Load<IgniteScriptableObject>("Skills/Ignite/Ignite2");
        GameObject ignite = Instantiate(ignitePrefab, transform.position, Quaternion.identity);
        ignite.GetComponent<Ignite>().ignite = igniteScriptableObject;
        ignite.GetComponent<Ignite>().direction = mousePosition;
    }
}
