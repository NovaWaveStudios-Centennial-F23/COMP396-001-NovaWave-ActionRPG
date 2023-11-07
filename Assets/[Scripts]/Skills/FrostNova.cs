using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNova : Skill
{
    IEnumerator Duration()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Duration());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
