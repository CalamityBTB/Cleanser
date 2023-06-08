using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBehaviour : MonoBehaviour
{

    public float Speed;

    private Transform target;

    private float shootingRange;
    private float distance;

    public Animator RoamerAnimator;
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        RoamerAnimator.SetFloat("X", horizontal);
        RoamerAnimator.SetFloat("Y", vertical);

        if (distance > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            


        }
        else if (distance <= shootingRange)
        {
            
        }



    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseArea);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
        
}


