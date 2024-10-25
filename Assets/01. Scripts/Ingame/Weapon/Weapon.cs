// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private float attackCool;
    [SerializeField] private float gaugeRange;

    public float AttackCool => attackCool;
    public float GaugeRange => gaugeRange;

    public abstract void Attack();
    public abstract void ChargedAttack();

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
