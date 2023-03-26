using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider Heaalthbar;
    public Health healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    

    private void Die()
    {
        
    } 
}
