// # Systems
using System;
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerWeaponButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDown;
    private bool isFull;

    //차지
    private float curChargeTime;
    private float maxChargeTime = 3;
    [SerializeField] private Image img_chargeGauge;

    private bool onAttack = true;
    private float maxCoolTime = 3;
    [SerializeField] private Image img_coolTimeGauge;

    [SerializeField] private Image img_button;

    private bool isBoost = false;

    private void Update()
    {
        if(isBoost)
        {
            return;
        } 

        if(isDown && !isFull)
        {
            curChargeTime += Time.deltaTime;
            img_chargeGauge.fillAmount = curChargeTime / maxChargeTime;

            if(curChargeTime >= maxChargeTime)
            {
                isFull = true;
            }
        }
    }

    public void SetBoostState(bool on)
    {
        isBoost = on;

        if (on)
        {
            img_chargeGauge.fillAmount = 1;
        }
        else
        {
            img_chargeGauge.fillAmount = 0;
        }
    }

    public void SetAttackCoolAndGauge(float cool, float charge)
    {
        maxCoolTime = cool;
        maxChargeTime = charge;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!onAttack) return;

        isDown = true;
        img_button.color = Color.gray;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!onAttack || !isDown) return;

        isDown = false;
        img_button.color = Color.white;

        if(!isBoost) img_chargeGauge.fillAmount = 0;
        curChargeTime = 0;

        if (isFull)
        {
            isFull = false;
            // 차지 공격
            GameManager.Instance.Weapon.ChargedAttack();
        }
        else
        {
            // 그냥 공격
            GameManager.Instance.Weapon.Attack();
        }

        StartCoroutine(AttackCool());
    }

    private IEnumerator AttackCool()
    {
        float time = 0;

        img_coolTimeGauge.fillAmount = 0;
        onAttack = false;

        while (time < maxCoolTime)
        {
            time += Time.deltaTime;
            float t = time / maxCoolTime;
            img_coolTimeGauge.fillAmount = t;
            yield return null;
        }

        img_coolTimeGauge.fillAmount = 1;
        onAttack = true;
    }
}
