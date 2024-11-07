using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeSize = 0.1f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;

        //StartCoroutine(Shake(shakeAmount, shakeTime));
    }

    public void StartShake(float setShakeTime = 1f)
    {
        StartCoroutine(ShakeStart(setShakeTime));
    }

    private IEnumerator ShakeStart(float duration)
    {
        float timer = 0;

        while (timer <= duration)
        {
            // y �� ���� ��鸮���� Random.Range�� ���
            float randomY = Random.Range(initialPosition.y - shakeSize, initialPosition.y + shakeSize);

            // ������ x, z ���� �����ϰ� y ���� ����
            transform.localPosition = new Vector3(0, randomY, -10);

            timer += Time.deltaTime;

            if (Time.timeScale == 0) break;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}
