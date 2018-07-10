﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneParticleActivate : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public GameObject instantiated;
    public float TimeWait;


    void Awake()
    {
        instantiated = (GameObject)Instantiate(ParticlePrefab, transform.position - new Vector3(-1, 4, 0), ParticlePrefab.transform.rotation);
        StartCoroutine("Timer");
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(TimeWait);
        Destroy(instantiated);
        gameObject.GetComponent<LineOfSight>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}