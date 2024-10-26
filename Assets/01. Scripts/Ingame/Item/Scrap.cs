// # Systems
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


// # Unity
using UnityEngine;
using UnityEngine.UIElements;

public class Scrap : MonoBehaviour
{
    private bool isDroped;
    private bool isMove;

    public bool IsDroped => isDroped;
    public bool IsMove => isMove;

    private Transform playerTransform;
    private Rigidbody2D rigid;

    private float speed = 1;
    private float accel = 15f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SpawnSpring();
    }

    private void FixedUpdate()
    {
        if(isMove && playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed * distanceToPlayer);
            speed += accel * Time.deltaTime;
        }
    }

    public void OnMoveWithPlayer(Transform playerTransform)
    {
        this.playerTransform = playerTransform;

        isMove = true;
    }

    private void SpawnSpring()
    {
        float randomX = Random.Range(4f, 8f);
        float randomY = Random.Range(4f, 8f);

        rigid.AddForce(new Vector2(randomX * 2, randomY * 2), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isDroped)
        {
            GameManager.Instance.GetScrap(5);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !isDroped)
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            isDroped = true;
        }
    }
}
