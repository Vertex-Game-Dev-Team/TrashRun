// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private GameObject  weapon;
    [SerializeField]
    private WeaponData  weaponData;

    private Animator    weaponAnimator;

    private void Start()
    {
        InitializeWeapon();

        weapon.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            AttackTo();
        }
    }

    private void InitializeWeapon()
    {
        weapon.gameObject.name = "Weapon :: " + weaponData.weaponID;

        weapon.GetComponent<SpriteRenderer>().sprite              = weaponData.weaponSprite;
        weapon.GetComponent<Animator>().runtimeAnimatorController = weaponData.weaponAnimator;

        weaponAnimator = weapon.GetComponent<Animator>();
    }

    private void AttackTo()
    {
        weapon.gameObject.SetActive(true);

        weaponAnimator.SetTrigger("Attack");
    }
}
