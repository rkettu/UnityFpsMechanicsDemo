using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBaseClass : MonoBehaviour
{
    protected float defaultDmg = 0f;
    protected float timeBetweenShots = 1f;
    protected static Camera myCam = null;

    // Observer pattern for updating UI when ammo data is changed
    public delegate void UpdateUI(int ammoHeldMax, int ammoHeldLeft, int ammoClipMax, int ammoClipLeft);
    public static event UpdateUI updateAmmoCount;

    // Choose how shoot function is called for fire1 and fire2
    public enum Fire1Mode { KeyDown, KeyHold };
    public enum Fire2Mode { KeyDown, KeyHold };

    private Fire1Mode fire1 = Fire1Mode.KeyDown;
    private Fire2Mode fire2 = Fire2Mode.KeyDown;

    private int m_ammoHeldMaxAmount;
    private int m_ammoHeldLeftAmount;
    private int m_ammoClipMaxAmount;
    private int m_ammoClipLeftAmount;

    public int ammoHeldMax
    {
        get
        {
            return m_ammoHeldMaxAmount;
        }
        protected set
        {
            m_ammoHeldMaxAmount = value;
            updateAmmoCount?.Invoke(m_ammoHeldMaxAmount, m_ammoHeldLeftAmount, m_ammoClipMaxAmount, m_ammoClipLeftAmount);
        }
    }

    public int ammoHeldLeft
    {
        get
        {
            return m_ammoHeldLeftAmount;
        }
        protected set
        {
            m_ammoHeldLeftAmount = value;
            updateAmmoCount?.Invoke(m_ammoHeldMaxAmount, m_ammoHeldLeftAmount, m_ammoClipMaxAmount, m_ammoClipLeftAmount);
        }
    }

    public int ammoClipMax
    {
        get
        {
            return m_ammoClipMaxAmount;
        }
        protected set
        {
            m_ammoClipMaxAmount = value;
            updateAmmoCount?.Invoke(m_ammoHeldMaxAmount, m_ammoHeldLeftAmount, m_ammoClipMaxAmount, m_ammoClipLeftAmount);
        }
    }
    public int ammoClipLeft
        {
        get
        {
            return m_ammoClipLeftAmount;
        }
        protected set
        {
            m_ammoClipLeftAmount = value;
            updateAmmoCount?.Invoke(m_ammoHeldMaxAmount, m_ammoHeldLeftAmount, m_ammoClipMaxAmount, m_ammoClipLeftAmount);
        }
    }

    private static Text clipText;
    private static Text ammoText;

    // Must be implemented - leave body empty for ones your weapon will not use
    public abstract void ShootPrimary();    // Called every frame key is held down or only when key is pressed down depending on Fire1Mode
    public abstract void ShootSecondary();
    public abstract void PrimaryFireKeyUp();    // Use for charged attacks etc (Shoot primary to increase damage over time, this function to fire)
    public abstract void SecondaryFireKeyUp();
    public abstract void Reload();
    public void InitActions()
    { 
        // Set camera if not already set
        if(myCam == null)
        {
            myCam = Camera.main;
        }
        // Update UI to reflect changed weapon ammocount
        updateAmmoCount?.Invoke(m_ammoHeldMaxAmount, m_ammoHeldLeftAmount, m_ammoClipMaxAmount, m_ammoClipLeftAmount);
    }
    protected abstract void ChildInitActions();

    public Fire1Mode getFire1Mode()
    {
        return fire1;
    }
    public void setFire1Mode(Fire1Mode newFire1)
    {
        fire1 = newFire1;
    }
    public Fire2Mode getFire2Mode()
    {
        return fire2;
    }
    public void setFire2Mode(Fire2Mode newFire2)
    {
        fire2 = newFire2;
    }
}
