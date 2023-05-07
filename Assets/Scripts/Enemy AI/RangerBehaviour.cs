using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBehaviour : MonoBehaviour
{
    [Header("Idle State")]
    [SerializeField] private float _idleMoveSpeed = 1.5f;
    [SerializeField] private float _idleMoveTimeMin = 2f;
    [SerializeField] private float _idleMoveTimeMax = 3f;

    [Header("Detection State")]
    [SerializeField] private float _detectionRadius = 10f;

    [Header("Combat State")]
    public GameObject poisonBallPrefab;
    [SerializeField] private float _poisonBallSpeed = 8f;
    [SerializeField] private float _poisonBallCooldown = 3f;
    [SerializeField] private float _poisonBallSpawnOffset = 1f;

    private Transform _target;
    private Vector3 _initialPosition;
    private bool _isIdle = true;
    private bool _isInCombat = false;
    private bool _isFiringPoisonBall = false;
    private float _nextPoisonBallTime;

    private Rigidbody2D rb;
    //private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        _initialPosition = transform.position;
        StartCoroutine(Idle());
    }

    private void Update()
    {
        if (_isInCombat)
        {
            if (Time.time > _nextPoisonBallTime && !_isFiringPoisonBall)
            {
                StartCoroutine(FirePoisonBall());
            }
        }
        else
        {
            if (_target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, _target.position);
                if (distanceToTarget <= _detectionRadius)
                {
                    _isIdle = false;
                    _isInCombat = true;
                    _isFiringPoisonBall = false;
                    StopAllCoroutines();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isIdle)
        {
            // Move randomly
            rb.velocity = Random.insideUnitCircle.normalized * _idleMoveSpeed;
        }
        else if (_isInCombat)
        {
            if (!_isFiringPoisonBall)
            {
                // Look at target
                Vector2 direction = (_target.position - transform.position).normalized;
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
            float idleMoveTime = Random.Range(_idleMoveTimeMin, _idleMoveTimeMax);
            yield return new WaitForSeconds(idleMoveTime);
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(idleMoveTime);
        }
    }

    IEnumerator FirePoisonBall()
    {
        _isFiringPoisonBall = true;
        //animasyon 
        yield return new WaitForSeconds(0.5f);
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + direction * _poisonBallSpawnOffset;

            GameObject poisonBall = Instantiate(poisonBallPrefab, spawnPosition, Quaternion.identity);
            poisonBall.GetComponent<Rigidbody2D>().velocity = direction * _poisonBallSpeed;

            _nextPoisonBallTime = Time.time + _poisonBallCooldown;
        }
        _isFiringPoisonBall = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _target = null;
            _isInCombat = false;
            StartCoroutine(Idle());
        }
    }
}


