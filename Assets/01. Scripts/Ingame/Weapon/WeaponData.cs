// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName ="Scriptable Object/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string                        weaponID;
    public Sprite                        weaponSprite;
    public RuntimeAnimatorController     weaponAnimator;

    [Space(10)]
    public float        weaponDamage;
}
