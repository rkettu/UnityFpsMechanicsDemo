using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assault rifle
public class Gun1Script : WeaponBaseClass
{
    private float reloadTime;
    private bool isReloading = false;
    private float effectiveRange = 20f;
    private float timer = 0f;
    void Start()
    {
        defaultDmg = 1.5f;
        reloadTime = 2f;
        timeBetweenShots = 0.05f;
        ammoClipMax = 30;
        ammoClipLeft = ammoClipMax;
        ammoHeldMax = 120;
        ammoHeldLeft = ammoHeldMax;
        setFire1Mode(Fire1Mode.KeyHold);
        setFire2Mode(Fire2Mode.KeyHold);
    }
    public override void ShootPrimary()
    {
        if(ammoClipLeft > 0 && !isReloading)
        {
            // A bit bad practice... makes weapon firing speed too dependent on framerate
            if (timer >= timeBetweenShots)
            {
                timer = 0f;
                ammoClipLeft--;
                RaycastHit hit;
                Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit);
                {
                    if (hit.collider != null)
                    {
                        // Looking for hits that hit an enemy with an associated health script
                        EnemyHealthScript enemyHealth = hit.transform.GetComponent<EnemyHealthScript>();
                        if (enemyHealth != null)
                        {
                            // Weapon deals half the damage from far away
                            float dealtDmg = hit.distance <= effectiveRange ? defaultDmg : (defaultDmg / 2f);
                            enemyHealth.TakeDamage(dealtDmg);
                        }
                    }
                }
            }
            else timer += Time.deltaTime;
        }       
    }
    public override void ShootSecondary()
    {
        // Special fire? shoot grenade etc
    }
    public override void Reload()
    {
        if(!isReloading && ammoClipLeft != ammoClipMax) StartCoroutine("ReloadCoroutine");
    }
    protected override void ChildInitActions()
    {
        isReloading = false;
        //UpdateAmmoUI();
    }

    public override void PrimaryFireKeyUp()
    {
    }
    public override void SecondaryFireKeyUp()
    {
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int reloadAmount = Mathf.Min(ammoClipMax - ammoClipLeft, ammoHeldLeft);
        ammoHeldLeft -= reloadAmount;
        ammoClipLeft += reloadAmount;

        //UpdateAmmoUI();
        isReloading = false;
    }
}
