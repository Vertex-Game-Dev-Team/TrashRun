// # Systems
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class _00_Logo : MonoBehaviour
{
    [Tooltip("�ΰ� ���� ���ʴ�� Fade�Ǹ� �������ϴ�.")]
    [SerializeField] private GameObject[] LogoArray;

    // �ΰ� �������� �ð�
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
