// # System
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


// # Unity
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    private float movementSpeed;

    [Header("Player Jump")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int jumpCount;

    private int maxJumpCount;
    private bool onJump;
    private bool isGround;
    [SerializeField] private Collider2D bodyCollider;

    private Vector2 ScrapCheckerOffset = new Vector2(-1, 0);
    private Vector2 ScrapCheckerSize = new Vector2(7, 3);

    private Rigidbody2D rigid;

    private Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxJumpCount = jumpCount;
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

        SetAnimParameter();
        CheckAroundScrap();
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

    private void CheckAroundScrap()
    {
        Collider2D[] scraps = Physics2D.OverlapBoxAll((Vector2)transform.position - ScrapCheckerOffset, ScrapCheckerSize, 0, LayerMask.GetMask("Scrap"));

        if(scraps.Length > 0)
        {
            for(int i = 0; i < scraps.Length; i++)
            {
                if (scraps[i].TryGetComponent(out Scrap scrap) && scrap.IsDroped && !scrap.IsMove)
                {
                    scrap.OnMoveWithPlayer(transform);
                }
            }
        }
    }

    private void SetAnimParameter()
    {
        animator.SetBool("IsJump", !isGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if (collision.transform.position.y + 0.5f < transform.position.y)
            {
                isGround = true;
                onJump = true;

                jumpCount = maxJumpCount;
            }
        }
    }
}
