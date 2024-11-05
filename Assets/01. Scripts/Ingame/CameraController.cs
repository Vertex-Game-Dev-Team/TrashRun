// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform   target;
    [SerializeField]
    private Vector2     offset;
    [SerializeField]
    private float       smoothSpeed;

    private float minOffsetY = 4.5f;
    private float maxOffsetY = 100;

    private bool isBoss;

    private void Start()
    {
        SetBossCamera();
    }

    private void LateUpdate()
    {
        if (!isBoss)
        {
            float y = Mathf.Clamp(target.position.y, minOffsetY, maxOffsetY);

            Vector3 targetPosition = new Vector3(target.position.x + offset.x, y, -10);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }
    }

    public void SetBossCamera()
    {
        StartCoroutine(CameraSizeUp());
    }

    private IEnumerator CameraSizeUp()
    {
        isBoss = true;

        float nowSize = Camera.main.orthographicSize;
        float targetSize = 7;

        float nowY = transform.position.y;
        float targetY = 6.5f;

        float time = 0;

        while(time < 3)
        {
            time += Time.deltaTime;

            float t = time / 3;

            Camera.main.orthographicSize = Mathf.Lerp(nowSize, targetSize, t);
            transform.position = new Vector3(target.position.x + offset.x, Mathf.Lerp(nowY, targetY, t),-10);

            yield return null;
        }

        transform.position = new Vector3(target.position.x + offset.x, targetY, -10);
        Camera.main.orthographicSize = targetSize;

        minOffsetY = targetY;
        maxOffsetY = targetY;
        isBoss = false;
    }
}