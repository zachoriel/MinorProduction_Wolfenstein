using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCode : MonoBehaviour
{
    public ControlPanel panel;
    public TextMesh codeText;
    public Color colors;

	// Use this for initialization
	void Start ()
    {
        colors.a = 0;
        codeText.color = colors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            panel.foundKeyCode = true;
            panel.objectiveText.text = "Objective: Enter the keycode!";
            panel.animator.SetTrigger("NewObjective");
            colors.a = 1;
            codeText.color = colors;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            colors.a = 0;
            codeText.color = colors;
        }
    }
}
