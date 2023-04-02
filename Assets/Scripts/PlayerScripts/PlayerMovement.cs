using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private SpriteRenderer spriteRenderer;
    public float speed;
    public Rigidbody2D rb;

    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;


    private bool canDash = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PerformDash());
        }

    }
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {

        data.playerPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement.normalized * speed;

        




    }

    IEnumerator PerformDash()
    {
        canDash = false; 

        
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Vector2 endPosition = rb.position + dashDirection * dashDistance;

        

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, endPosition, elapsedTime / dashDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;




    }
}