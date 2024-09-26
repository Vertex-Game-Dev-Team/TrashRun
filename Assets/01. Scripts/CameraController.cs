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

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + offset.y, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition   = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position       = smoothedPosition;
    }
}