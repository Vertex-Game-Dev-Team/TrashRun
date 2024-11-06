using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonsterBase
{
    private float moveSpeed = 9;
    private float[] movePointY = { 3, 6, 9, 12 };

    private bool unMove;

    private Rigidbody2D rigid;
    private Animator animator;
    [SerializeField] private GameObject[] spawnMonsters;

    [SerializeField] private Animator warningAnim;

    private Coroutine mainCoroutine;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        hp = 10;
    }

    private void Start()
    {
        mainCoroutine = StartCoroutine(PatternCycle());
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void OnHit()
    {
        base.OnHit();

        if (mainCoroutine != null) StopCoroutine(mainCoroutine);
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        unMove = false;

        mainCoroutine = StartCoroutine(PatternCycle());
        moveCoroutine = StartCoroutine(ReturnTo(new Vector3(Camera.main.transform.position.x + 20, transform.position.y, 0)));
    }

    private IEnumerator ReturnTo(Vector3 targetPos)
    {
        float moveTime = 0;
        float maxTime = 1f;
        Vector3 startPos = transform.position;

        while (moveTime < maxTime)
        {
            moveTime += Time.deltaTime;
            float t = moveTime / maxTime;

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }
    }

    private void Move()
    {
        if (unMove) return;

        rigid.velocity = Vector2.right * moveSpeed;
    }

    private IEnumerator PatternCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            int ran = Random.Range(0, 2);

            if (ran == 0)
            {
                yield return Pattern_1();
            }
            else if (ran == 1)
            {
                yield return Pattern_2();
            }
        }
    }

    // 이동하면서 몬스터 소환
    private IEnumerator Pattern_1()
    {
        unMove = true;

        int rand;

        yield return moveCoroutine = StartCoroutine(MoveToLine(0));
        rand = Random.Range(0, spawnMonsters.Length);
        Instantiate(spawnMonsters[rand], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.3f);

        yield return moveCoroutine = StartCoroutine(MoveToLine(1));
       rand = Random.Range(0, spawnMonsters.Length);
        Instantiate(spawnMonsters[rand], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.3f);

        yield return moveCoroutine = StartCoroutine(MoveToLine(2));
        rand = Random.Range(0, spawnMonsters.Length);
        Instantiate(spawnMonsters[rand], transform.position, Quaternion.identity);

        unMove = false;
    }

    private IEnumerator MoveToLine(int lineIndex)
    {
        float moveTime = 0;
        float maxTime = 0.3f;
        Vector3 startPos = transform.position;

        while (moveTime < maxTime)
        {
            moveTime += Time.deltaTime;
            float t = moveTime / maxTime;

            transform.position = Vector3.Lerp(startPos, new Vector3(Camera.main.transform.position.x + 9, movePointY[lineIndex], 0), t);

            yield return null;
        }
    }

    // 돌진
    private IEnumerator Pattern_2()
    {
        float moveTime = 0;
        float maxTime = 0.3f;
        float startPosY = transform.position.y;

        while (moveTime < maxTime)
        {
            moveTime += Time.deltaTime;
            float t = moveTime / maxTime;

            transform.position = Vector3.Lerp(new Vector3(transform.position.x, startPosY, 0), new Vector3(Camera.main.transform.position.x + 9, GameManager.Instance.Player.transform.position.y, 0), t);

            yield return null;
        }

        warningAnim.SetTrigger("Play");
        animator.SetTrigger("Pat_2");

        unMove = true;

        rigid.velocity = Vector2.right * 12;
        yield return new WaitForSeconds(1f);

        rigid.velocity = Vector2.left * 20;
        yield return new WaitForSeconds(1f);

        moveCoroutine = StartCoroutine(ReturnTo(new Vector3(Camera.main.transform.position.x + 16, transform.position.y, 0)));
        unMove = false;
    }
}
