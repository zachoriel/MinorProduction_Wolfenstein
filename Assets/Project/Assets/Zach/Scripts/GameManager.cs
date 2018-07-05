using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraScript camera;
    public WeaponSwitch weapons;
    public GameObject pauseScreen, sceneFaderCanvas, reticle;
    public AudioSource pauseMusic;
    private bool slowMotion = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            camera.SetCursorLock(false);
            camera.enabled = false;
            weapons.enabled = false;
            pauseScreen.SetActive(true);
            sceneFaderCanvas.SetActive(false);
            reticle.SetActive(false);
            pauseMusic.Play();
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (slowMotion == false)
            {
                slowMotion = true;
                Time.timeScale = 0.35f;
            }
            else
            {
                slowMotion = false;
                Time.timeScale = 1f;
            }
        }
    }
}
