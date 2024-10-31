// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;


// # Unity
using UnityEngine;

public class RushMonster : MonsterBase
{
    private bool inFrontOfPlayer;
    private bool isRush;

    [SerializeField] private Animator warningAnim;

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
            inFrontOfPlayer = Physics2D.OverlapBox(transform.position + new Vector3(-10, 0, 0), new Vector2(12, 5), 0, LayerMask.GetMask("PlayerBody"));
        }
    }

    private IEnumerator Rush()
    {
        warningAnim.SetTrigger("Play");

        GetComponent<Animator>().SetTrigger("JumpJump");

        yield return new WaitForSeconds(1f);

        GetComponent<Rigidbody2D>().velocity = Vector2.left * 15;
    }
}
