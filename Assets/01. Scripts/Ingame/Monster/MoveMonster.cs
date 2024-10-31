using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonsterBase
{
    public float speed = 1.0f;       // ������ �ӵ�
    public float height = 1.0f;      // ������ ����
    private Vector3 startPosition;   // ���� ��ġ

    void Start()
    {
        // ���� ��ġ�� �����մϴ�.
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.Sin�� ����Ͽ� ���Ʒ��� �����̵��� �����մϴ�.
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPosition + new Vector3(0, newY, 0);
    }
}
