// # Systems
using Newtonsoft.Json.Linq;
using System.Collections;
using TMPro;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class IngameUIMnager : MonoBehaviour
{
    [SerializeField] private PlayerJoycon joycon;
    [SerializeField] private PlayerWeaponButton weaponButton;
    [SerializeField] private GameObject boostBorder;

    public PlayerWeaponButton WeaponButton => weaponButton;
    private Weapon myWeapon;
    [SerializeField] private TMP_Text txt_gaugeUpSize;

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
    private int xValue;

    public void UpGaugeValue(float value)
    {
        curBoostValue += value;
        boostGauge.fillAmount = curBoostValue / maxBoostValue;

        txt_gaugeUpSize.GetComponent<Animator>().SetTrigger("Play");
        if (!isBoost)
        {
            xValue += 1;
            txt_gaugeUpSize.text = "X" + xValue;
        }
        GameManager.Instance.Player.UpSpeed(curBoostValue / maxBoostValue);

        if (curBoostValue >= maxBoostValue)
        {
            txt_gaugeUpSize.text = "Fever!!";
            xValue = 0;
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
        boostBorder.gameObject.SetActive(true);

        GameManager.Instance.Player.boostEffect = Instantiate(EffectManager.instance.boostEffect, GameManager.Instance.Player.boostEffectPos.position, Quaternion.identity);
        GameManager.Instance.Player.boostEffect.transform.SetParent(GameManager.Instance.Player.transform);

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

        Destroy(GameManager.Instance.Player.boostEffect);
        boostBorder.gameObject.SetActive(false);

        boostGauge.fillAmount = 0;
        weaponButton.SetBoostState(false);
        weaponButton.SetAttackCoolAndGauge(myWeapon.AttackCool, myWeapon.ChargeGaugeRange);
        GameManager.Instance.SetBoostState(false);
        isBoost = false;
    }
    #endregion
}
