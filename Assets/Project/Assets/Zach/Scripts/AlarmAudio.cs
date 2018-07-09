using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmAudio : MonoBehaviour
{
    public GameObject player; 
    public float minDist;
    public float maxDist;
    private float currentDist;
    public AudioSource alarm;
    // Use this for initialization
    void Awake()
    {
        currentDist = (this.transform.position - player.transform.position).magnitude;
        alarm = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDist = (this.transform.position - player.transform.position).magnitude;
        AdjustVolume();
    }

    private void AdjustVolume()
    {
        float distance = Mathf.Clamp(currentDist, minDist, maxDist);
        float volume = 1.0f - ((distance - minDist) / (maxDist - minDist));
        alarm.volume = volume;
    }
}
