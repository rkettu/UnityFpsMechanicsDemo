using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] float health = 100f;
    public Slider healthSlider;

    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Self-explanatory...
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthSlider.value -= amount;
        if(health <= 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        //play death animation and related sounds first
        Destroy(gameObject);
    }
}
