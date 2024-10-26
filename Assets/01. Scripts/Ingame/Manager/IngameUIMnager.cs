// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


// # Unity
using UnityEngine;
using UnityEngine.UI;

public class IngameUIMnager : MonoBehaviour
{
    [SerializeField] private PlayerJoycon joycon;
    [SerializeField] private PlayerWeaponButton weaponButton;
    private Weapon myWeapon;

    private void Start()
    {
        boostGauge.fillAmount = 0;
        myWeapon = GameManager.Instance.Weapon;

        weaponButton.SetAttackCoolAndGauge(myWeapon.AttackCool, myWeapon.ChargeGaugeRange);
    }

    #region Boost
    [SerializeField] private Image boostGauge;
    private float curBoostValue = 0;
    private readonly float maxBoostValue = 100;
    private bool isBoost;

    public void UpGaugeValue(float value)
    {
        curBoostValue += value;
        boostGauge.fillAmount = curBoostValue / maxBoostValue;

        GameManager.Instance.Player.UpSpeed(curBoostValue / maxBoostValue);

        if (curBoostValue >= maxBoostValue)
        {
            curBoostValue = maxBoostValue;
            if (!isBoost)
            {
                isBoost = true;
                StartCoroutine(Co_Boosting());
            }
        }
    }

    private IEnumerator Co_Boosting()
    {
        weaponButton.SetBoostState(true);
        weaponButton.SetAttackCoolAndGauge(myWeapon.AttackCool * 1 / 4, 0);
        GameManager.Instance.SetBoostState(true);

        while (isBoost)
        {
            // 100 => 4초동안 모두 줄어들기
            curBoostValue -= Time.deltaTime * 25;
            boostGauge.fillAmount = curBoostValue / maxBoostValue;

            if (curBoostValue <= 0)
            {
                break;
            }

            yield return null;
        }

        boostGauge.fillAmount = 0;
        weaponButton.SetBoostState(false);
        weaponButton.SetAttackCoolAndGauge(myWeapon.AttackCool, myWeapon.ChargeGaugeRange);
        GameManager.Instance.SetBoostState(false);
        isBoost = false;
    }
    #endregion
}
