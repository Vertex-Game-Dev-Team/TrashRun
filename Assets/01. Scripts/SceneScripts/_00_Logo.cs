// # Systems
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class _00_Logo : MonoBehaviour
{
    [Tooltip("로고를 넣은 차례대로 Fade되며 보여집니다.")]
    [SerializeField] private GameObject[] LogoArray;

    // 로고가 보여지는 시간
    private const float showTime = 2;

    private void Start()
    {
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        for(int i = 0; i < LogoArray.Length; i++)
        {
            LogoArray[i].SetActive(true);
            Fade.In(Fade.FADE_TIME_DEFAULT);
            yield return new WaitForSeconds(0.5f);

            yield return new WaitForSeconds(showTime);

            Fade.Out(Fade.FADE_TIME_DEFAULT);
            yield return new WaitForSeconds(0.5f);
            LogoArray[i].SetActive(false);
        }

        SceneManager.LoadScene(SceneNameString._01_Title);
    }
}
