using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [Header("Component Setup")]
    public Animator animator;
    public TextMesh codeText;
    public WeaponSwitch weapons;

    [Header("UI")]
    public GameObject pinPadUI;
    public Text objectiveText;

    [Header("Input")]
    public InputField inputField;
    public Text playerInput;
    public GameObject pinPadWall; // Temporarily for destroying wall while we come up with a better way of doing this

    [Header("Audio")]
    public AudioSource incorrectCode;

    [Header("Numbers")]
    public int keyCode; // Could be private, but helpful for testing

    [HideInInspector]
    public bool enteredKeyCode, foundKeyCode;

    private bool foundControlPanel, panelIsActive;



	// Use this for initialization 
	void Start ()
    {
        foundControlPanel = false;
        foundKeyCode = false;
        enteredKeyCode = false;
        panelIsActive = false;

        keyCode = Random.Range(1000, 9999);
        codeText.text = keyCode.ToString();
	}

    // Update is called once per frame
    void Update()
    {
        if (panelIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (playerInput.text.ToString() == keyCode.ToString())
                {
                    enteredKeyCode = true;
                    inputField.text = null;
                    inputField.DeactivateInputField();
                    pinPadUI.SetActive(false);
                    panelIsActive = false;
                    weapons.enabled = true;
                    Destroy(pinPadWall);
                    Destroy(gameObject);

                    objectiveText.text = "Objective: Escape!";
                    animator.SetTrigger("NewObjective");
                }
                else
                {
                    inputField.text = null;
                    inputField.ActivateInputField();
                    incorrectCode.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (enteredKeyCode == true)
            {
                return;
            }

            if (foundControlPanel == false && foundKeyCode == false)
            {
                foundControlPanel = true;
                objectiveText.text = "Objective: Find the keycode!";
                animator.SetTrigger("NewObjective");
            }
            else if (foundControlPanel == false && foundKeyCode == true)
            {
                foundControlPanel = true;
            }
            else if (foundControlPanel == true)
            {
                pinPadUI.SetActive(true);
                weapons.enabled = false;
                panelIsActive = true;
                inputField.text = null;
                inputField.ActivateInputField();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (enteredKeyCode == true)
            {
                return;
            }

            if (foundControlPanel == true)
            {
                pinPadUI.SetActive(false);
                weapons.enabled = true;
                panelIsActive = false;
                inputField.text = null;
                inputField.DeactivateInputField();
            }
        }
    }
}
