using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    protected Vector2 checkBoxOffset;
    protected Vector2 checkBoxSize;

    private void CheckAttackRange()
    {
        Collider2D[] MonsterColliders = Physics2D.OverlapBoxAll(checkBoxOffset, checkBoxSize, 0, LayerMask.GetMask("Monster"));

        foreach(Collider2D collider in MonsterColliders)
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkBoxOffset, checkBoxSize);
    }
}
