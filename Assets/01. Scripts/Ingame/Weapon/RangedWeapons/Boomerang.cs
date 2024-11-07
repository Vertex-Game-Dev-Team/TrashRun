using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    private bool isReturn;
    [SerializeField] private bool isCharged;

    private void Start()
    {
        StartCoroutine(Co_WaitReturn());
    }

    private void FixedUpdate()
    {
        if(!isReturn)
        {
            rigid.velocity = Vector2.right * (isCharged ? 30 : 20);
        }
        else
        {
            rigid.velocity = Vector2.left * (isCharged ? 20 : 15);
        }
    }

    IEnumerator Co_WaitReturn()
    {
        yield return new WaitForSeconds(1.5f);

        isReturn = true;
    }
}
