using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Text volumeText;

    // Use this for initialization
    void Start()
    {        
        AudioListener.volume = 0.50f;
        volumeText.text = AudioListener.volume.ToString();
    }

    public void UpVol()
    {
        AudioListener.volume += 0.10f;
        if (AudioListener.volume >= 1.0f)
        {
            AudioListener.volume = 1.0f;
        }
        volumeText.text = AudioListener.volume.ToString();
    }

    public void DownVol()
    {
        AudioListener.volume -= 0.10f;
        if (AudioListener.volume <= 0.0f)
        {
            AudioListener.volume = 0.0f;
        }
        volumeText.text = AudioListener.volume.ToString();
    }
}
