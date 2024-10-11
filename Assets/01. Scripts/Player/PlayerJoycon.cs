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
    private float joyRangeY = 100f; // joy의 이동범위 최소 최대 Y;

    private Vector2 defaultJoyPosition; // 기본 joy 위치
    private Vector2 startPointerPosition; // 엄지를 누른 위치
    private Vector2 currentPointerPosition;

    private void Awake()
    {
        joyRect = GetComponent<RectTransform>();
        defaultJoyPosition = joyRect.anchoredPosition; // 초기 조이콘 위치 저장
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
        // 현재 마우스/터치 위치와 시작 위치의 차이 계산 (Y축)
        float deltaY = currentPointerPosition.y - startPointerPosition.y;

        // 새로운 Y값 계산 (이동 범위 제한 포함)
        float newYPosition = Mathf.Clamp(defaultJoyPosition.y + deltaY, defaultJoyPosition.y - joyRangeY, defaultJoyPosition.y + joyRangeY);

        // 조이콘 위치 업데이트 (X축은 그대로, Y축만 이동)
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