using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchgun : MonoBehaviour
{
    private const int MAX_WEAPONS = 5;

    // Array holding weapons that the player can switch to with numkeys
    public GameObject[] weaponsArray = new GameObject[MAX_WEAPONS];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Looking for player NumKey input
    // Should probably also have ShootPrimary, Reload, etc. function calls here...
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Since the gameobject has script that inherits GunBaseScript we actually call the inheriting scripts implementation...
            // Maybe make an array of GunBaseScript objects instead so we don't need to GetComponent everytime
            weaponsArray[0].GetComponent<GunBaseScript>().MyTestFunction();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponsArray[1].GetComponent<GunBaseScript>().MyTestFunction();
        }
        /*
         * 
         * etc
         *
         */ 
    }

    // ADD FUNCTION FOR UPDATING WEAPONSARRAY
    // SO PLAYER CAN CHANGE WEAPONS FROM MENU ETC
}
