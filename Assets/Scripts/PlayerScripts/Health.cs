using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    [SerializeField]public float CurrentHealth;
    public Image HealthBarFill;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar();
    }

    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateHealthBar();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateHealthBar()
    {
        float HealthPercentage = CurrentHealth / 100f;
        HealthBarFill.fillAmount = HealthPercentage;
    }
    

    private void Die()
    {
        
    } 
}
