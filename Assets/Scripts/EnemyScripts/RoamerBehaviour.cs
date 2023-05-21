using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerBehaviour : MonoBehaviour
{
    [Header("Idle State")]
    [SerializeField] private float _roamRadius = 5f;
    [SerializeField] private float _moveSpeed = 2f;
    private bool _isIdle = true;

    [Header("Combat State")]
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _lastAttackTime = 0f;
    [SerializeField] private float _stunDuration = 1f;
    [SerializeField] private float _chaseSpeed = 3f;

    private bool _isInCombat = false;
    private bool _isStunned = false;
    private bool _isChasing = false;

    private Vector3 _initialPosition;
    private Vector3 _lastTargetPosition;
    private Transform _target;

    Health playerHealth;

    public float RoamerDamage;

    void Start()
    {
        _initialPosition = transform.position;
        StartCoroutine(Roaming());
    }

    private IEnumerator Roaming()
    {
        while (true)
        {
            if (_isIdle)
            {
                RoamAround();
            }
            else if (_isInCombat)
            {
                if (_target != null)
                {
                    if (!_isChasing)
                    {
                        _isChasing = true;
                        _lastTargetPosition = _target.position;
                    }
                    ChaseTarget();
                   
                }
                else
                {

                    _isIdle = true;
                }
            }
            else if (_isStunned)
            {

                StopAllCoroutines();
                yield return new WaitForSeconds(_stunDuration);
                _isStunned = false;
                StartCoroutine(Roaming());
            }

            yield return null;
        }
    }



    void RoamAround()
    {
        if (Random.Range(0, 100) < 10)
        {
            Vector2 randomDirection = Random.insideUnitCircle * _roamRadius;
            Vector3 targetPosition = _initialPosition + new Vector3(randomDirection.x, randomDirection.y, 0f);
            StartCoroutine(MoveToTargetPosition(targetPosition));
        }
    }

    IEnumerator MoveToTargetPosition(Vector3 targetPosition)
    {
        _isIdle = false;
        float distance = Vector3.Distance(transform.position, targetPosition);
        while (distance > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }
        _isIdle = true;
        
    }

    void ChaseTarget()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.position;
            if (!_isChasing)
            {
                _isChasing = true;
                _lastTargetPosition = targetPosition;
            }
            if (_lastTargetPosition != targetPosition)
            {
                _lastTargetPosition = targetPosition;
            }
            transform.position = Vector3.MoveTowards(transform.position, _lastTargetPosition, _chaseSpeed * Time.deltaTime);
        }
    }


   

    void AttackTarget()
    {
        
        if (Time.time > _lastAttackTime + _attackCooldown)
        {
            // Play attack animation, reduce player health or use block mechanic
            _lastAttackTime = Time.time;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        _isInCombat = false;
        yield return new WaitForSeconds(_attackCooldown);
        _isInCombat = true;
    }


    public void Stun()
    {
        if (!_isStunned)
        {
            //yapacaksak stun animasyonu
            _isStunned = true;
            StartCoroutine(StunCooldown());
        }
    }

    IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(_stunDuration);
        _isStunned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _target = collision.transform;
            _isIdle = false;
            _isInCombat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _target = null;
            _isChasing = false;
            StopAllCoroutines();
            StartCoroutine(MoveToTargetPosition(_initialPosition));
            StartCoroutine(Roaming());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().CurrentHealth -= RoamerDamage;
            AttackTarget();
                   
        }
    }
    

}