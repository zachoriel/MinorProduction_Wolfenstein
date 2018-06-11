using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string levelToLoad = "ShipLevel";

    public SceneFader sceneFader;
    public GameObject settingsMenu;
    public GameObject menuButtons;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && settingsMenu.activeInHierarchy)
        {
            Back();
        }
    }

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Settings()
    {
        menuButtons.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        menuButtons.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
