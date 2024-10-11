// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    private float movementSpeed;

    [Header("Player Jump")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int jumpCount;

    private int defaultJumpCount;
    private bool onJump;
    private bool isGround;
    private Collider2D bodyCollider;

    private Rigidbody2D rigid;

    private void Awake()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        defaultJumpCount = jumpCount;
    }

    private void Update()
    {
        // Å×½ºÆ®
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Down();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigid.velocity = new Vector2(Vector2.right.x * movementSpeed, rigid.velocity.y);
    }

    public void Jump()
    {
        if(jumpCount > 0 && onJump)
        {
            StartCoroutine(JumpCoolTime());

            isGround = false;
            jumpCount--;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
    }

    public void Down()
    {
        if(isGround && transform.position.y > 4)
        {
            isGround = false;
            StartCoroutine(TurnOffBodyCollider());
        }
    }

    private IEnumerator TurnOffBodyCollider()
    {
        bodyCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        bodyCollider.enabled = true;
    }

    private IEnumerator JumpCoolTime()
    {
        onJump = false;
        yield return new WaitForSeconds(0.4f);
        onJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if (collision.transform.position.y + 0.5f < transform.position.y)
            {
                isGround = true;
                onJump = true;

                jumpCount = defaultJumpCount;
            }
        }
    }
}
