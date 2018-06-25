using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraScript camera;
    public WeaponSwitch weapons;
    public GameObject pauseScreen;
    public AudioSource pauseMusic;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            camera.SetCursorLock(false);
            camera.enabled = false;
            weapons.enabled = false;
            pauseScreen.SetActive(true);
            pauseMusic.Play();
            Time.timeScale = 0;
        }
    }
}
