using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Text ammoText;
    public Gun mg;
    public Shotgun sg;
    public LaserRifle lr;

    [Header("Current Weapon")]
    public int selectedWeapon = 0;

	// Use this for initialization
	void Start ()
    {
        //SelectWeapon();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int previousSelectedWeapon = selectedWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;

            if (i == 1)
            {
                ammoText.text = mg.currentGunAmmoMG.ToString() + " / " + mg.totalAmmoMG;
            }
            else if (i == 2)
            {
                ammoText.text = sg.currentGunAmmoSG.ToString() + " / " + sg.totalAmmoSG;
            }
            else if (i == 3)
            {
                ammoText.text = lr.energy.ToString();
            }
            else if (i == 4)
            {
                ammoText.text = "NA";
            }
        }
    }
}
