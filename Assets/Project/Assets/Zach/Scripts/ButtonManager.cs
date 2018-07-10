﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public CameraScript camera;
    public WeaponSwitch weapons;
    public GameObject pauseScreen, sceneFaderCanvas, reticle;
    public AudioSource pauseMusic;
    public Animator animator;
    public SceneFader fader;
    public GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        camera.enabled = true;
        camera.SetCursorLock(true);
        weapons.enabled = true;
        pauseScreen.SetActive(false);
        sceneFaderCanvas.SetActive(true);
        reticle.SetActive(true);
        pauseMusic.Stop();
        manager.isPaused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        camera.enabled = true;
        camera.SetCursorLock(true);
        sceneFaderCanvas.SetActive(true);
        reticle.SetActive(true);
        manager.isPaused = false;
        Time.timeScale = 1;
        fader.FadeTo("ShipLevel");
        //SceneManager.LoadScene("ShipLevel");
        pauseMusic.Stop();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Return()
    {
        fader.FadeTo("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }

}
