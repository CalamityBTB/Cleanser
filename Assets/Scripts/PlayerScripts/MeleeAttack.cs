using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Health
{
    public float AttackRange = 0.5f; 
    private float attackDamage = 5f; 
    public LayerMask EnemyLayers;

    
    
    private bool _isAttacking = false;

    public AudioSource Swing1;
    public AudioSource Swing2;
    public AudioSource SwordHit1;
    public AudioSource SwordHit2;
    public AudioSource Block1;
    public AudioSource Block2;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isAttacking )
        {
            
           Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, AttackRange);

          foreach (Collider2D hitObject in hitObjects)
          {
                    if (hitObject.tag == "Enemy")
                    {
                        Enemy enemy = hitObject.GetComponent<Enemy>();
                        if (enemy != null)
                        {

                            PlayRandomHitSound();
                            enemy.TakeDamage(attackDamage);
       
                        }
                    }
          }
                
           
            
        }
       
       
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    private void PlayRandomHitSound()
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            SwordHit1.Play();
        }
        else
        {
            SwordHit2.Play();
        }
    }
    private void PlayRandomSwingSound()
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            Swing1.Play();
        }
        else
        {
            Swing2.Play();
        }
    }
   
}

    

    
    


