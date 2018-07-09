using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnityEditorWindow : EditorWindow
{
    private GameObject godLight;

    void Awake()
    {
        godLight = GameObject.FindGameObjectWithTag("GodLight");
    }

    [MenuItem("Tools/CustomWindow")]
    public static void CreateWindow()
    {
        EditorWindow.GetWindow<UnityEditorWindow>(); 
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Toggle God Light"))
        {
            if (godLight.activeInHierarchy)
            {
                godLight.SetActive(false);
            }
            else
            {
                godLight.SetActive(true);
            }
        }
    }
}
