using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float destroyTime = 5;
    protected Rigidbody2D rigid;
    private List<Collider2D> collider2Ds = new List<Collider2D>();
    private int canHit = 1; // 몬스터 한마리당 처맞을 수 있는 수

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            int hitCount = 0;

            for(int i =0; i < collider2Ds.Count; i++)
            {
                if (collider2Ds[i] == collision)
                {
                    hitCount++;
                }
            }

            if (hitCount >= canHit) return;

            collision.gameObject.GetComponent<MonsterBase>().OnHit();

            collider2Ds.Add(collision);
        }
    }
}
