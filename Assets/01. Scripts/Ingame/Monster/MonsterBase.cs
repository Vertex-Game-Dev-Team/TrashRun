using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    private int hp = 1;
    [SerializeField] private Sprite hitSprtie;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnHit()
    {
        hp--;

        if (hp <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    private IEnumerator Dead()
    {
        spriteRenderer.sprite = hitSprtie;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        DroppedScrap();
        Destroy(gameObject);
    }

    private void DroppedScrap()
    {
        for(int i = 0; i < 3; i++)
        {
            DroppedItemManager.Instance.SpawnScrap(transform.position);
        }
    }
}
