using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBehaviour : MonoBehaviour
{
    public float Speed;

    private Transform target;

    [SerializeField]private float RotationSpeed = 5f;

    private float chaseArea;
    private float shootingRange;
    private float distance;

    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);


        if (distance < chaseArea && distance > shootingRange)
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


