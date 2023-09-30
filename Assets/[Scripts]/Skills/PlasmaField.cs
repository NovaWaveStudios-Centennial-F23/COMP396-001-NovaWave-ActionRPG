using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaField : MonoBehaviour
{
    public PlasmaFieldScriptableObject plasmaField;
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(plasmaField.duration);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnOff());
    }
}
