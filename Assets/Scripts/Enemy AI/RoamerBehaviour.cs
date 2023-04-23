using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerBehaviour : MonoBehaviour
{
    [SerializeField] private float roamRadius = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float stunDuration = 1f;
    [SerializeField] private float chaseSpeed = 3f;

    private Vector3 initialPosition;
    private bool isIdle = true;
    private bool isInCombat = false;
    private bool isStunned = false;
    private float lastAttackTime = 0f;
    private Transform target;
    private bool isChasing = false;
    private Vector3 lastTargetPosition;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(Roaming());
    }

    private IEnumerator Roaming()
    {
        while (true)
        {
            if (isIdle)
            {
                RoamAround();
            }
            else if (isInCombat)
            {
                if (target != null)
                {
                    if (!isChasing)
                    {
                        isChasing = true;
                        lastTargetPosition = target.position;
                    }
                    ChaseTarget();
                   
                }
                else
                {

                    isIdle = true;
                }
            }
            else if (isStunned)
            {

                StopAllCoroutines();
                yield return new WaitForSeconds(stunDuration);
                isStunned = false;
                StartCoroutine(Roaming());
            }

            yield return null;
        }
    }



    void RoamAround()
    {
        if (Random.Range(0, 100) < 10)
        {
            Vector2 randomDirection = Random.insideUnitCircle * roamRadius;
            Vector3 targetPosition = initialPosition + new Vector3(randomDirection.x, randomDirection.y, 0f);
            StartCoroutine(MoveToTargetPosition(targetPosition));
        }
    }

    IEnumerator MoveToTargetPosition(Vector3 targetPosition)
    {
        isIdle = false;
        float distance = Vector3.Distance(transform.position, targetPosition);
        while (distance > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }
        isIdle = true;
        
    }

    void ChaseTarget()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            if (!isChasing)
            {
                isChasing = true;
                lastTargetPosition = targetPosition;
            }
            if (lastTargetPosition != targetPosition)
            {
                lastTargetPosition = targetPosition;
            }
            transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, chaseSpeed * Time.deltaTime);
        }
    }


   

    void AttackTarget()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            // Play attack animation, reduce player health or use block mechanic
            lastAttackTime = Time.time;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        isInCombat = false;
        yield return new WaitForSeconds(attackCooldown);
        isInCombat = true;
    }


    public void Stun()
    {
        if (!isStunned)
        {
            //yapacaksak stun animasyonu
            isStunned = true;
            StartCoroutine(StunCooldown());
        }
    }

    IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            isIdle = false;
            isInCombat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
            isChasing = false;
            StopAllCoroutines();
            StartCoroutine(MoveToTargetPosition(initialPosition));
            StartCoroutine(Roaming());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AttackTarget();
        }
    }


}