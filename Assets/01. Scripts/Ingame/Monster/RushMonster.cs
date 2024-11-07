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
    private bool realRush;

    [SerializeField] private Animator warningAnim;

    private void Update()
    {
        if (realRush)
        {
            transform.Translate(Vector2.left * 15 * Time.deltaTime) ;
            return;
        }

        if(inFrontOfPlayer && !isRush)
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

        realRush = true;
    }
}
