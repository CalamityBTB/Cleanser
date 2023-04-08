using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerBehaviour : MonoBehaviour
{
    public float roamRadius = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public float stunDuration = 1f;
    public float chaseSpeed = 3f; 

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
    }

    void Update()
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
                if (IsTargetWithinAttackRange())
                {
                    AttackTarget();
                }
            }
        }
        else if (isStunned)
        {

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
        Vector3 targetPosition = target.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        lastTargetPosition = targetPosition;
    }

    bool IsTargetWithinAttackRange()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            return distance <= attackRange;
        }
        return false;
    }

    void AttackTarget()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            //atak animasyonu
            //player can azalmasý veya block mekaniði
            lastAttackTime = Time.time;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        isInCombat = false;
        yield return new WaitForSeconds(2f);
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
        if (collision.tag == "Player")
        {
            target = collision.transform;
            isIdle = false;
            isInCombat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
            isChasing = false; 
            
            StartCoroutine(MoveToTargetPosition(initialPosition));
        }
    }
}