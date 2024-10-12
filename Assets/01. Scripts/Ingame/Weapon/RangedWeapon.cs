// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject chargedBullet;

    private Vector3 bulletSpawnOffset = new Vector3(1.5f, -0.5f, 0);

    public override void Attack()
    {
        Instantiate(bullet, transform.position + bulletSpawnOffset, Quaternion.identity);
    }

    public override void ChargedAttack()
    {
        Instantiate(chargedBullet, transform.position + bulletSpawnOffset, Quaternion.identity);
    }
}
