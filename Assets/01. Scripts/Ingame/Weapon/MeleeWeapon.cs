using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("CheckBox")]
    [SerializeField] protected Vector2 attackCheckBoxOffset;
    [SerializeField] protected Vector2 attackCheckBoxSize;

    [Header("CheckBox_Charged")]
    [SerializeField] protected Vector2 chargedCheckBoxOffset;
    [SerializeField] protected Vector2 chargedCheckBoxSize;

    public override void Attack()
    {
        if (isBoost)
        {
            ChargedAttack();
            return;
        }

        animator.SetTrigger("NormalAttack");

        // �ڽ� Ŭ�������� ����
        foreach (Collider2D collider in GetMonstersWithAttack())
        {
            if(collider.transform.TryGetComponent(out MonsterBase monster))
            {
                monster.OnHit();
            }
        }
    }

    public override void ChargedAttack()
    {
        animator.SetTrigger("ChargingAttack");

        // �ڽ� Ŭ�������� ����
        foreach (Collider2D collider in GetMonstersWithChargedAttack())
        {
            if (collider.transform.TryGetComponent(out MonsterBase monster))
            {
                monster.OnHit();
            }
        }
    }

    protected Collider2D[] GetMonstersWithAttack()
    {
        Collider2D[] MonsterColliders = Physics2D.OverlapBoxAll((Vector2)transform.position + attackCheckBoxOffset, attackCheckBoxSize, 0, LayerMask.GetMask("Monster"));

        return MonsterColliders;
    }

    protected Collider2D[] GetMonstersWithChargedAttack()
    {
        Collider2D[] MonsterColliders = Physics2D.OverlapBoxAll((Vector2)transform.position + chargedCheckBoxOffset, chargedCheckBoxSize, 0, LayerMask.GetMask("Monster"));

        return MonsterColliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)attackCheckBoxOffset, attackCheckBoxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3)chargedCheckBoxOffset, chargedCheckBoxSize);
    }
}
