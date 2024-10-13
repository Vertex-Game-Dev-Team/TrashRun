// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class RushMonster : MonsterBase
{
    private bool inFrontOfPlayer;
    private bool isRush;

    private void Update()
    {
        if (isRush) return;

        if(inFrontOfPlayer)
        {
            isRush = true;
            StartCoroutine(Rush());
        }
        else
        {
            inFrontOfPlayer = Physics2D.Raycast(transform.position, Vector2.left, 10, LayerMask.GetMask("PlayerBody"));
        }
    }

    private IEnumerator Rush()
    {
        yield return null;
    }
}
