using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    //物理判定の更新の毎に呼ぶ必要がある
    public bool IsGround(){
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            //Debug.Log("何かが侵入しました");

            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            //Debug.Log("何かが居続けています");

            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == groundTag) {
            //Debug.Log("何かが出ました");
            isGroundExit = true;
        }
    }

}
