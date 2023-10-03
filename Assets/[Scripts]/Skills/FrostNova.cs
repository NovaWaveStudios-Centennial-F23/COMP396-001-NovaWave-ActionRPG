using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNova : MonoBehaviour
{
    public FrostNovaScriptableObject frostNova;
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(frostNova.duration);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnOff());
    }
}
