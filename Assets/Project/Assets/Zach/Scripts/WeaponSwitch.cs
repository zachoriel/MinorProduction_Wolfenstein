using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Text ammoText;
    public Gun mg;
    public Shotgun sg;
    public LaserRifle lr;

    public bool hasPickedUpGun = false;

    [Header("Current Weapon")]
    public int selectedWeapon = 0;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        int previousSelectedWeapon = selectedWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (hasPickedUpGun == true)
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
            else
            {
                return;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (hasPickedUpGun == true)
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
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && hasPickedUpGun == true)
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && hasPickedUpGun == true)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && hasPickedUpGun == true)
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
                ammoText.text = lr.energy.ToString() + "%";
            }
            else if (i == 4)
            {
                ammoText.text = "NA";
            }
        }
    }
}
