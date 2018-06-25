using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public CameraScript camera;
    public WeaponSwitch weapons;
    public GameObject pauseScreen;
    public AudioSource pauseMusic;
    public Animator animator;

    public void Resume()
    {
        camera.enabled = true;
        camera.SetCursorLock(true);
        weapons.enabled = true;
        pauseScreen.SetActive(false);
        pauseMusic.Stop();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        camera.enabled = true;
        camera.SetCursorLock(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("ShipLevel");
        pauseMusic.Stop();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
