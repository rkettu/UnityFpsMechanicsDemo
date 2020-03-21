using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtherControls : MonoBehaviour
{
    private const int MAX_WEAPONS = 3;
    public GameObject[] weaponsArray = new GameObject[MAX_WEAPONS];
    private WeaponBaseClass[] weaponScripts = new WeaponBaseClass[MAX_WEAPONS];
    private WeaponBaseClass.Fire1Mode fire1Down = WeaponBaseClass.Fire1Mode.KeyDown;
    private WeaponBaseClass.Fire2Mode fire2Down = WeaponBaseClass.Fire2Mode.KeyDown;

    private int currentWeaponIndex = 0;
    void Start()
    {
        UpdateWeapons(weaponsArray);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponScripts[currentWeaponIndex].Reload();
        }

        // Using GetButton or GetButtonDown depending on which is set by the weapon script, Fire Mode is KeyDown by default
        if (weaponScripts[currentWeaponIndex].getFire1Mode() == fire1Down ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1"))
        {
            weaponScripts[currentWeaponIndex].ShootPrimary();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            weaponScripts[currentWeaponIndex].PrimaryFireKeyUp();
        }
        if (weaponScripts[currentWeaponIndex].getFire2Mode() == fire2Down ? Input.GetButtonDown("Fire2") : Input.GetButton("Fire2"))
        {
            weaponScripts[currentWeaponIndex].ShootSecondary();
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            weaponScripts[currentWeaponIndex].SecondaryFireKeyUp();
        }
    
        
        // Check if pressed numkeys between Num1 and Num(max_weapons)
        for(int i = 0; i < MAX_WEAPONS; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if(i != currentWeaponIndex)
                {
                    currentWeaponIndex = i;
                    ActivateWeapon();
                }
            }
        }

    } 
    
    // Call from menu etc to switch weapon order
    // Updates both weapons gameobject arrays and specific weapon script arrays
    // Also sets first weapon as the active one
    // NOTE: In a game with a lot of weapons, maybe Destroy weapons not set and Instantiate only the ones in the current array
    public void UpdateWeapons(GameObject[] newWeaponsArray)
    {
        currentWeaponIndex = 0;

        for(int i = 0; i < weaponsArray.Length; i++)
        {
            weaponsArray[i] = newWeaponsArray[i];
            weaponScripts[i] = newWeaponsArray[i].GetComponent<WeaponBaseClass>();
        }

        Debug.Log("Weapons list updated");

        ActivateWeapon();
    }

    private void ActivateWeapon()
    {
        for(int i = 0; i < weaponsArray.Length; i++)
        {
            if (i == currentWeaponIndex)
            {
                weaponsArray[i].SetActive(true);
                weaponScripts[i].InitActions();
            }
            else
            {
                weaponsArray[i].SetActive(false);
            }
        }

        Debug.Log("Weapon activated: " + (currentWeaponIndex + 1));
    }
}
