using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndControlsSetting : MonoBehaviour
{
    public bool aimAssist = true;
    public bool invertedMouse = false;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
    public void ChangeAimSetting()
    {
        if (aimAssist == true)
        {
            aimAssist = false;
        }
        else
        {
            aimAssist = true;
        }
    }

    public void ChangeInvertedSetting()
    {
        if (invertedMouse == false)
        {
            invertedMouse = true;
        }
        else
        {
            invertedMouse = false;
        }
    }
}
