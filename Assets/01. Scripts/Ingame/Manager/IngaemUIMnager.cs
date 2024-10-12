// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class IngaemUIMnager : MonoBehaviour
{
    [SerializeField] private PlayerJoycon joycon;
    [SerializeField] private PlayerWeaponButton weaponButton;

    private void Start()
    {
        Weapon myWeapon = GameManager.Instance.Weapon;
        weaponButton.SetAttackCoolAndGauge(myWeapon.AttackCool, myWeapon.GaugeRange);
    }
}
