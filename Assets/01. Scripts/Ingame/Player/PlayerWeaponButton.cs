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
    [SerializeField] private Image chargeGauge;

    private bool onAttack = true;
    private float maxCoolTime = 3;
    [SerializeField] private Image coolTimeGauge;

    private void Update()
    {
        if(isDown && !isFull)
        {
            curChargeTime += Time.deltaTime;
            chargeGauge.fillAmount = curChargeTime / maxChargeTime;

            if(curChargeTime >= maxChargeTime)
            {
                isFull = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!onAttack) return;

        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!onAttack) return;

        isDown = false;

        chargeGauge.fillAmount = 0;
        curChargeTime = 0;

        if (isFull)
        {
            isFull = false;
            // 차지 공격
        }
        else
        {
            // 그냥 공격
        }

        StartCoroutine(AttackCool());
    }

    private IEnumerator AttackCool()
    {
        float time = 0;

        coolTimeGauge.fillAmount = 0;
        onAttack = false;

        while (time < maxCoolTime)
        {
            time += Time.deltaTime;
            float t = time / maxCoolTime;
            coolTimeGauge.fillAmount = t;
            yield return null;
        }

        coolTimeGauge.fillAmount = 1;
        onAttack = true;
    }
}
