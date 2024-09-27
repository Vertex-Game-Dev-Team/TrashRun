// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    private float       movementSpeed;

    [Header("Player Jump")]
    [SerializeField]
    private float       jumpForce;
    [SerializeField]
    private int         jumpCount;

    private int         defaultJumpCount;
    private bool        isJump;

    private Rigidbody2D rigid;

    private void Start()
    {
        defaultJumpCount = jumpCount;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (jumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if(isJump)
        {
            Jump();
        }
    }

    private void Move()
    {
        rigid.velocity = new Vector2(Vector2.right.x * movementSpeed, rigid.velocity.y);
    }

    public void Jump()
    {
        isJump = false;

        jumpCount--;

        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = defaultJumpCount;
        }
    }
}
