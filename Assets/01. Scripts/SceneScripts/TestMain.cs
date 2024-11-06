using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMain : MonoBehaviour
{
    [SerializeField] private Button btn_pipe;
    [SerializeField] private Button btn_bat;
    [SerializeField] private Button btn_boomerang;

    private void Awake()
    {
        btn_pipe.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 0); SceneManager.LoadScene(SceneNameString._03_Game); });
        btn_bat.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 1); SceneManager.LoadScene(SceneNameString._03_Game); });
        btn_boomerang.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 2); SceneManager.LoadScene(SceneNameString._03_Game); });
    }

}
