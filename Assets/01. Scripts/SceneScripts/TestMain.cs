using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using EasyTransition;

public class TestMain : MonoBehaviour
{
    [SerializeField] private Button btn_pipe;
    [SerializeField] private Button btn_bat;
    [SerializeField] private Button btn_boomerang;

    public TransitionSettings transition;

    private void Awake()
    {
        btn_pipe.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 0); TransitionManager.Instance().Transition(SceneNameString._03_Game, transition, 0); });
        btn_bat.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 1); TransitionManager.Instance().Transition(SceneNameString._03_Game, transition, 0); });
        btn_boomerang.onClick.AddListener(() => { PlayerPrefs.SetInt(PlayerPrefsKey.WeaponID, 2); TransitionManager.Instance().Transition(SceneNameString._03_Game, transition, 0); });
    }

}
