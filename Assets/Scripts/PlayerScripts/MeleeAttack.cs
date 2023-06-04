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
    private int _currentComboHits = 0;
    private bool _isComboActive = false;
    private float _lastComboTime = 0f;
    public float BlockDuration = 0.5f;
    private float _lastBlockTime = 0f;
    private bool _isBlocking = false;
    private bool _isAttacking = false;

    public AudioSource Swing1;
    public AudioSource Swing2;
    public AudioSource SwordHit1;
    public AudioSource SwordHit2;
    public AudioSource Block1;
    public AudioSource Block2;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isAttacking && !_isBlocking)
        {
            if (_isComboActive && Time.time - _lastComboTime < ComboDelay && _currentComboHits < MaxComboHits)
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
                            enemy.TakeDamage(AttackDamage);
                        }
                    }
                }

                _currentComboHits++;
                _lastComboTime = Time.time;
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
                            PlayRandomHitSound();
                            enemy.TakeDamage(AttackDamage);
                        }
                    }
                    else
                    {
                        PlayRandomSwingSound();
                    }
                }

                _currentComboHits = 1;
                _isComboActive = true;
                _lastComboTime = Time.time;
            }

            if (Time.time - _lastComboTime > ComboDelay || _currentComboHits >= MaxComboHits)
            {
                _isComboActive = false;
                _currentComboHits = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !_isAttacking)
        {
            _lastBlockTime = Time.time;
            _isBlocking = true;
            Debug.Log("block on");
        }

        if (Time.time - _lastBlockTime > BlockDuration)
        {
            _isBlocking = false;
            Debug.Log("block off");
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
    private void PlayRandomBlockSound()
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            Block1.Play();
        }
        else
        {
            Block2.Play();
        }
    }
}

    

    
    


