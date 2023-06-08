using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBehaviour : MonoBehaviour
{

    public float Speed;

    private Transform target;

    private float shootingRange = 5f;
    
    private float fireRate = 1f;

    public Transform[] FirePoint;
    public GameObject[] PoisonBallPrefab;
    public int maxPoisonBall = 10;


    public Animator RoamerAnimator;
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        RoamerAnimator.SetFloat("X", horizontal);
        RoamerAnimator.SetFloat("Y", vertical);

        if (target != null)
        {
            Invoke("Fire", fireRate);
        }

        
        



    }
    private void Fire()
    {
        

        for (int i = 0; i < maxPoisonBall + 1; i++)
        {
            float bulDirX = transform.position.x;
            float bulDirY = transform.position.y;

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstanse.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            
        }
    }


        private void OnDrawGizmosSelected()
        {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        }
        
}


