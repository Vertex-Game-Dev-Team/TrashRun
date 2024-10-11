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

    private void LateUpdate()
    {
        Vector3 targetPosition   = new Vector3(target.position.x + offset.x, offset.y, -10);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position       = smoothedPosition;
    }
}