using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour     
{
    public GameObject trailRenderer;
    private SpriteRenderer spriteRenderer;
    public float speed;
    public Rigidbody2D rb;

    public Animator animator;

    //Dash 
    private float dashCooldownTime;
    private float dashTime;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;

    private float activeMoveSpeed;

    public float dashSpeed;
    private bool isDashing = false;
   


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        dashTime = dashDuration;
        dashCooldownTime = dashCooldown;
        activeMoveSpeed = speed;
    }

    private void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement.normalized * activeMoveSpeed;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime >= dashCooldown)
        {
            isDashing = true;
            activeMoveSpeed = dashSpeed;
            dashTime = dashDuration;
            dashCooldownTime = 0;

            if (isDashing)
            {
                trailRenderer.SetActive(true);
                
            }
            
           
            
            
        }
        



        if (dashTime <= 0)
        {
            activeMoveSpeed = speed;
        }

        else
        {
            dashTime -= Time.deltaTime;
        }

        dashCooldownTime += Time.deltaTime;
    
    }
    

}