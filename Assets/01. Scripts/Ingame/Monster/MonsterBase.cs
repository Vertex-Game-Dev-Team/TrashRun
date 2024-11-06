using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    protected int hp = 1;
    [SerializeField] private Sprite hitSprtie;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 7f);
    }

    public virtual void OnHit()
    {
        hp--;

        StartCoroutine(HitStop());
    }

    private IEnumerator HitStop()
    {
        Sprite defaultSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = hitSprtie;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.25f);
        
        spriteRenderer.sprite = defaultSprite;
        Time.timeScale = 1;

        if(hp <= 0)
        {
            DroppedScrap();
            Destroy(gameObject);
        }
    }

    private void DroppedScrap()
    {
        for(int i = 0; i < 3; i++)
        {
            DroppedItemManager.Instance.SpawnScrap(transform.position);
        }
    }

    private void OnEnable()
    {
        if (spriteRenderer != null) return;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
