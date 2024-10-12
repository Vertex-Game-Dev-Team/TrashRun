// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// # Unity
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJoycon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private PlayerMovement player;
    private RectTransform joyRect;
    private bool isThumbDown;
    private float joyRangeY = 100f; // joy�� �̵����� �ּ� �ִ� Y;

    private Vector2 defaultJoyPosition; // �⺻ joy ��ġ
    private Vector2 startPointerPosition; // ������ ���� ��ġ
    private Vector2 currentPointerPosition;

    private void Awake()
    {
        joyRect = GetComponent<RectTransform>();
        defaultJoyPosition = joyRect.anchoredPosition; // �ʱ� ������ ��ġ ����
    }

    private void Update()
    {
        if (isThumbDown)
        {
            JoyMove();
            PlayerMove();
        }
    }

    private void PlayerMove()
    {
        if(joyRect.anchoredPosition.y == joyRangeY)
        {
            player.Jump();
        }
        else if(joyRect.anchoredPosition.y == -joyRangeY)
        {
            player.Down();
        }
    }

    private void JoyMove()
    {
        // ���� �����ϰ��ִ� ��ġ�� Ŭ���� ��ġ�� �Ÿ��� ���
        float deltaY = currentPointerPosition.y - startPointerPosition.y;

        // ���� ���� ��ġ + �Ÿ���  newYPosition�� ����
        float newYPosition = Mathf.Clamp(defaultJoyPosition.y + deltaY, defaultJoyPosition.y - joyRangeY, defaultJoyPosition.y + joyRangeY);

        // ���� �̵�
        joyRect.anchoredPosition = new Vector2(defaultJoyPosition.x, newYPosition); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isThumbDown = true;
        startPointerPosition = eventData.position;
        currentPointerPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isThumbDown = false;
        joyRect.anchoredPosition = defaultJoyPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPointerPosition = eventData.position;
    }
}