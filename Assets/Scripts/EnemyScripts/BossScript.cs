using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossScript : MonoBehaviour
{
    
    private Vector3 lastTargetPosition;
    public Transform target;
    public GameObject Boss;

    private Rigidbody2D bossBody;
    private Vector2 movement;
    [SerializeField]private float moveSpeed = 5f;
    private bool fastUpgrade = false;

    private Vector3 dashDir;
    private float dashSpeed = 6f;
    public float bossDamage = 20f;
    public float BossHealth = 200f;


    void Start()
    {
        bossBody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bossBody.rotation = angle;
        direction.Normalize();
        movement = direction;
        transform.Translate(direction * moveSpeed * Time.deltaTime);


        if (BossHealth <= 70f)
        {
            fastUpgrade = true;
            StartCoroutine(Dash(direction));
        }


    }
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    public void TakeDamage(float damage)
    {
        BossHealth -= damage;

        if (BossHealth <= 0)
        {
            Destroy(Boss);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        bossBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    IEnumerator Dash(Vector3 direction)
    {
        
        for (int i = 3; i < 1; i--)
        {
            yield return new WaitForSeconds(2);
            moveSpeed = moveSpeed* dashSpeed;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bossDamage);
            }
        }
    }

}
