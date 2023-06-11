using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBall : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    //public float damage = 10f;

    private Vector3 direction;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 shootDirection)
    {
        direction = shootDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player health down
            Destroy(gameObject);
        }
    }
}

