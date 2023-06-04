using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public AudioSource Die1;
    public AudioSource Die2;
    public AudioSource Die3;
    public void TakeDamage(float Damage)
    {
        health -= Damage;

        if (health <= 0f)
        {
            PlayRandomDeathSound();
            Destroy(gameObject);
        }
    }
    private void PlayRandomDeathSound()
    {
        int randomValue = Random.Range(0, 3);
        if (randomValue <= 1)
        {
            Die1.Play();
        }
        else if (randomValue > 1 && randomValue <= 2)
        {
            Die2.Play();
        }
        else
        {
            Die3.Play();
        }
    }
}
