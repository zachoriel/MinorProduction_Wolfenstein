using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerStats player;
    public CameraScript camera;
    public WeaponSwitch weapons;
    public GameObject pauseScreen, sceneFaderCanvas, reticle;
    public AudioSource pauseMusic;
    public AudioSource enterSlowMo;
    public AudioSource exitSlowMo;
    public Text scoreText;
    public bool inCredits = false;
    private bool slowMotion = false;

    void Awake()
    {
        if (inCredits)
        {
            player = FindObjectOfType<PlayerStats>();
        }
    }

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
                exitSlowMo.Stop();
                enterSlowMo.Play();
                slowMotion = true;
                Time.timeScale = 0.35f;
            }
            else
            {
                enterSlowMo.Stop();
                exitSlowMo.Play();
                slowMotion = false;
                Time.timeScale = 1f;
            }
        }

        if (inCredits)
        {
            scoreText.text = "Score: " + player.Score;
        }
    }
}
