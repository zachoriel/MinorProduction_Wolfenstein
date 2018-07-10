using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablelooping : MonoBehaviour
{
    public ParticleSystem psyst;
    public float TimeWait;

    void Start()
    {
        StartCoroutine("Timer");
    }



    IEnumerator Timer()
    {
        yield return new WaitForSeconds(TimeWait);
        psyst = this.gameObject.GetComponent<ParticleSystem>();
        var main = psyst.main;
        main.loop = false;
    }

}
