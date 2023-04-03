using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Health
{
    public float AttackRange = 0.5f; 
    public float AttackDamage = 10; 
    public LayerMask EnemyLayers;
    public float ComboDelay = 0.5f; 
    public int MaxComboHits = 3; 
    private int CurrentComboHits = 0; 
    private bool isComboActive = false;
    private float LastComboTime = 0f;
    

    private bool isAttacking = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking) 
        {
            
            
                if (isComboActive && Time.time - LastComboTime < ComboDelay && CurrentComboHits < MaxComboHits)
                {
                   
                    Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, AttackRange);

                    
                    foreach (Collider2D hitObject in hitObjects)
                    {
                        if (hitObject.tag == "Enemy")
                        {
                            Enemy enemy = hitObject.GetComponent<Enemy>();
                            if (enemy != null)
                            {
                                enemy.TakeDamage(AttackDamage);
                            }
                        }
                    }

                    CurrentComboHits++;
                    LastComboTime = Time.time;
                  Debug.Log("combo done");
                }
                else
                {
                    
                    Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, AttackRange);

                   
                    foreach (Collider2D hitObject in hitObjects)
                    {
                        if (hitObject.tag == "Enemy")
                        {
                            
                            Enemy enemy = hitObject.GetComponent<Enemy>();
                            if (enemy != null)
                            {
                                enemy.TakeDamage(AttackDamage);
                            }
                        }
                    }

                    CurrentComboHits = 1;
                    isComboActive = true;
                    LastComboTime = Time.time;
                }
            

            if (Time.time - LastComboTime > ComboDelay || CurrentComboHits >= MaxComboHits)
            {
                isComboActive = false;
                CurrentComboHits = 0;
            }
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}

    

    
    


