using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float speed;
    public Rigidbody2D rb;

    public float dashDistance;
    public float DashDuration;

    private bool isDashing = false;

    private void Start()
    {

        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement.normalized * speed;


        
        if (!isDashing)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            }


        }


    }


    private IEnumerator Dash()
    {

        isDashing = true;

        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = dashDirection * dashDistance / DashDuration;

        yield return new WaitForSeconds(DashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;


    }



}
