// # Systems
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using EasyTransition;


// # Unity
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _01_Title : MonoBehaviour
{
    [SerializeField] private TMP_Text txt_TouchToStart;

    public TransitionSettings transitionSettings;

    private void Start()
    {
        StartCoroutine(TextFadeRepeat());
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            //SceneManager.LoadScene(SceneNameString._02_Main);
            TransitionManager.Instance().Transition("TestMain", transitionSettings, 0);
        }
    }

    private IEnumerator TextFadeRepeat()
    {
        float time = 0;
        float fadeTime = 1;

        bool flag = false;

        while (true)
        {
            float a = time / fadeTime;
            txt_TouchToStart.color = new Color(1,1,1, a);

            if(flag)
            {
                time -= Time.deltaTime;

                if(time <= 0)
                {
                    flag = false;
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time >= fadeTime)
                {
                    yield return new WaitForSeconds(2);
                    flag = true;
                }
            }

            yield return null;
        }
    }
}
