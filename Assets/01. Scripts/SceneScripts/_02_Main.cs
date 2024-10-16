// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class _02_Main : MonoBehaviour
{
    [SerializeField] private GameObject pnl_mailbox;
    [SerializeField] private GameObject pnl_mode;
    [SerializeField] private GameObject pnl_weapon;
    [SerializeField] private GameObject pnl_characteristic;
    [SerializeField] private GameObject pnl_option;

    [SerializeField] private Button btn_mailbox;
    [SerializeField] private Button btn_mode;
    [SerializeField] private Button btn_weapon;
    [SerializeField] private Button btn_characteristic;
    [SerializeField] private Button btn_option;

    [SerializeField] private Button[] btns_backToMain;

    private void Awake()
    {
        OnClickButttons();
    }

    private void OnClickButttons()
    {
        btn_mailbox.onClick.AddListener(() => { ShowPanel(pnl_mailbox); });
        btn_characteristic.onClick.AddListener(() => { ShowPanel(pnl_characteristic); });
        btn_option.onClick.AddListener(() => { ShowPanel(pnl_option); });
        btn_mode.onClick.AddListener(() => { ShowPanel(pnl_mode); });
        btn_weapon.onClick.AddListener(() => { ShowPanel(pnl_weapon); });

        for(int i = 0; btns_backToMain.Length > i; i++)
        {
            btns_backToMain[i].onClick.AddListener(() => { ShowPanel(null); });
        }
    }

    private void ShowPanel(GameObject panel)
    {
        pnl_mailbox.SetActive(false);
        pnl_characteristic.SetActive(false);
        pnl_mode.SetActive(false);
        pnl_weapon.SetActive(false);
        pnl_option.SetActive(false);

        if (panel != null) panel.SetActive(true);
    }
}
