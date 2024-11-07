using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private Transform[] backgrounds;
    public bool IsStop;

    private void FixedUpdate()
    {
        if (IsStop) return;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].Translate(Vector2.left * speed);
            if (backgrounds[i].localPosition.x <= -39)
            {
                backgrounds[i].localPosition = new Vector3(39, -2.8f, 0);
            }
        }
    }
}