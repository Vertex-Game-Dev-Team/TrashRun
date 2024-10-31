using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonsterBase
{
    public float speed = 1.0f;       // 움직임 속도
    public float height = 1.0f;      // 움직임 높이
    private Vector3 startPosition;   // 시작 위치

    void Start()
    {
        // 시작 위치를 저장합니다.
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.Sin을 사용하여 위아래로 움직이도록 설정합니다.
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPosition + new Vector3(0, newY, 0);
    }
}
