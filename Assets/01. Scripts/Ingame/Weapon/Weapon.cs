// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private float attackCool;
    [SerializeField] private float chargeGaugeRange;

    public float AttackCool => attackCool;
    public float ChargeGaugeRange => chargeGaugeRange;

    protected bool isBoost;

    public abstract void Attack();
    public abstract void ChargedAttack();

    protected Animator animator;

    public void SetBoostState(bool on)
    {
        isBoost = on;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
