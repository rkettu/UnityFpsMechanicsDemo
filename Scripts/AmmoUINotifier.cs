using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUINotifier : MonoBehaviour
{
    public Text myClipText;
    public Text myAmmoText;
    void Start()
    {
        WeaponBaseClass.updateAmmoCount += UpdateAmmoCount;
    }

    void UpdateAmmoCount(int ammoHeldMaxAmount, int ammoHeldLeftAmount, int ammoClipMaxAmount, int ammoClipLeftAmount)
    {
        myClipText.text = ammoClipMaxAmount > 0 ? ammoClipLeftAmount.ToString() : "";
        myAmmoText.text = ammoHeldMaxAmount > 0 ? ammoHeldLeftAmount.ToString() : "";
    }

    private void OnDisable()
    {
        WeaponBaseClass.updateAmmoCount -= UpdateAmmoCount;
    }
}
