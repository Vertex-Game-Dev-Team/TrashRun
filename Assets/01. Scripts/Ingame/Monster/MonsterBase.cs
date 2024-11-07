using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using USF.Manager.SoundManager;

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
        spriteRenderer.color = Color.red;

        Instantiate(EffectManager.instance.boomEffect, transform.position, Quaternion.identity);
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.15f);
        
        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.color = Color.white;
        Time.timeScale = 1;

        if(hp <= 0)
        {
            SoundManager.instance.PlaySFX("MonsterDie");

            DroppedScrap();

            yield return new WaitForSeconds(0.1f);
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
