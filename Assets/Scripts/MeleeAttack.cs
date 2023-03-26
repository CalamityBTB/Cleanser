using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 0.5f; 
    public int attackDamage = 10; 
    public LayerMask enemyLayers; 

    private bool isAttacking = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) 
        {
            StartCoroutine(PerformAttack()); 
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

       
        foreach (Collider2D enemy in hitEnemies)
        {
           
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }

        
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
