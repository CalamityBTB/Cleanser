using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBehaviour : MonoBehaviour
{
    [Header("Idle State")]
    public float idleMoveSpeed = 1.5f;
    public float idleMoveTimeMin = 2f;
    public float idleMoveTimeMax = 3f;

    [Header("Detection State")]
    public float detectionRadius = 10f;

    [Header("Combat State")]
    public GameObject poisonBallPrefab;
    public float poisonBallSpeed = 8f;
    public float poisonBallCooldown = 3f;
    public float poisonBallSpawnOffset = 1f;

    private Transform target;
    private Vector3 initialPosition;
    private bool isIdle = true;
    private bool isInCombat = false;
    private bool isFiringPoisonBall = false;
    private float nextPoisonBallTime;

    private Rigidbody2D rb;
    //private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        initialPosition = transform.position;
        StartCoroutine(Idle());
    }

    private void Update()
    {
        if (isInCombat)
        {
            if (Time.time > nextPoisonBallTime && !isFiringPoisonBall)
            {
                StartCoroutine(FirePoisonBall());
            }
        }
        else
        {
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget <= detectionRadius)
                {
                    isIdle = false;
                    isInCombat = true;
                    isFiringPoisonBall = false;
                    StopAllCoroutines();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            // Move randomly
            rb.velocity = Random.insideUnitCircle.normalized * idleMoveSpeed;
        }
        else if (isInCombat)
        {
            if (!isFiringPoisonBall)
            {
                // Look at target
                Vector2 direction = (target.position - transform.position).normalized;
                transform.right = direction;

                // Stop moving
                rb.velocity = Vector2.zero;
            }
        }
    }

    IEnumerator Idle()
    {
        while (true)
        {
            float idleMoveTime = Random.Range(idleMoveTimeMin, idleMoveTimeMax);
            yield return new WaitForSeconds(idleMoveTime);
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(idleMoveTime);
        }
    }

    IEnumerator FirePoisonBall()
    {
        isFiringPoisonBall = true;
        //animasyon 
        yield return new WaitForSeconds(0.5f);
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + direction * poisonBallSpawnOffset;

            GameObject poisonBall = Instantiate(poisonBallPrefab, spawnPosition, Quaternion.identity);
            poisonBall.GetComponent<Rigidbody2D>().velocity = direction * poisonBallSpeed;

            nextPoisonBallTime = Time.time + poisonBallCooldown;
        }
        isFiringPoisonBall = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
            isInCombat = false;
            StartCoroutine(Idle());
        }
    }
}


