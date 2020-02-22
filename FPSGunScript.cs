using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public ParticleSystem muzzleFlash;
    public Text clipAmmoText;
    public Text ammoLeftText;

    [SerializeField] float defaultDmg = 20f;
    [SerializeField] float effectiveRange = 30f;
    [SerializeField] int maxAmmo = 50;
    [SerializeField] int clipSize = 6;
    private int ammoInClip;
    [SerializeField] float shotCooldown = 0.5f;     // seconds between shots
    private float timeSinceLastShot = 0;
    private int ammoLeft;
    private bool isReloading = false;
    [SerializeField] float reloadTime = 1.5f;
   
    void Start()
    {
        // = Gun is fully loaded
        ammoInClip = clipSize;
        ammoLeft = maxAmmo;
        clipAmmoText.text = ammoInClip.ToString();
        ammoLeftText.text = ammoLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isReloading)
        {
            Shoot();
        }
    }

    // Currently possible as fast as you click
    void Shoot()
    {
        if (ammoInClip > 0)
        {
            ammoInClip -= 1;
            clipAmmoText.text = ammoInClip.ToString();
            muzzleFlash.Play();

            RaycastHit hit;
            // Shooting a ray from camera, out is a C# keyword for passing by reference
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
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
        // clip empty
        else
        {
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        transform.Rotate(Vector3.right * -30f);
        yield return new WaitForSeconds(reloadTime);
        transform.Rotate(Vector3.right * 30f);
        int amount = Mathf.Min(ammoLeft, clipSize);
        ammoLeft -= amount;
        clipAmmoText.text = amount.ToString();
        ammoLeftText.text = ammoLeft.ToString();
        ammoInClip = amount;
        isReloading = false;
    }
}
