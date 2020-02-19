using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public ParticleSystem muzzleFlash;
    [SerializeField] float defaultDmg = 20f;
    [SerializeField] float effectiveRange = 30f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // Currently possible as fast as you click
    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        // Shooting a ray from camera, out is a C# keyword for passing by reference
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            // Looking for hits that hit an enemy with an associated health script
            EnemyHealthScript enemyHealth = hit.transform.GetComponent<EnemyHealthScript>();
            if(enemyHealth != null)
            {
                // Weapon deals half the damage from far away
                float dealtDmg = hit.distance <= effectiveRange ? defaultDmg : (defaultDmg / 2f);
                enemyHealth.TakeDamage(dealtDmg);
            }
        }
    }
}
