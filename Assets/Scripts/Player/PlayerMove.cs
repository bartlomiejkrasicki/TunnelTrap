using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerSpeed = 70;
    public bool facingRight = true;
    public int playerJumpPower = 1200;
    public float moveX;
    public bool isGrounded;
    public bool inCrawlingPosition = false;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded==true)
        {
            Jump();
            isGrounded = false;
        }
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        ChangePositionToCrawl();
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = facingRight;
    }

    void ChangePositionToCrawl()
    {
        if (Input.GetKey(KeyCode.C))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            inCrawlingPosition = true;
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            inCrawlingPosition = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
