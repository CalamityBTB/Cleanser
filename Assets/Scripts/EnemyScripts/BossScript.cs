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
    public float BossHealth = 400f;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;
    public float BulletSpeed = 10f;
    public float BulletFireRate = 0.2f;

    private bool isBulletHell = false;

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

  
    void Start()
    {
        bossBody = GetComponent<Rigidbody2D>();
        InvokeRepeating("Fire", 0f, 2f);
    }

    void Update()
    {

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bossBody.rotation = angle;
        direction.Normalize();
        movement = direction;
        transform.Translate(direction * moveSpeed * Time.deltaTime);


        if (BossHealth <= 350f && !isBulletHell)
        {
            fastUpgrade = true;
            StartCoroutine(Dash(direction));
            
        }
        if(BossHealth <= 270f)
        {
            
        }
        if (BossHealth <= 120f)
        {
            StartCoroutine(Explode());
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

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstanse.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += angleStep;
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
    IEnumerator Explode()
    {
        moveSpeed = moveSpeed * dashSpeed;
        //animasyon
        yield return new WaitForSeconds(3f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Health playerHealth = collider.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(bossDamage);
                }
            }
        }
        yield return new WaitForSeconds(4f);
    }


}
