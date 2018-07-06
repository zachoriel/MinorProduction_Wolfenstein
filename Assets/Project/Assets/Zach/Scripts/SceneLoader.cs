using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string levelToLoad = "ShipLevel";

    public SceneFader sceneFader;
    public GameObject settingsMenu;
    public GameObject instructionsMenu;
    public GameObject menuButtons;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && settingsMenu.activeInHierarchy)
        {
            Back();
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && instructionsMenu.activeInHierarchy)
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

    public void Instructions()
    {
        menuButtons.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void Back()
    {
        menuButtons.SetActive(true);
        
        if (settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
        }

        if (instructionsMenu.activeInHierarchy)
        {
            instructionsMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
