using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    private string groundTag = "ground";
    private string movefloorTag = "movefloor";
    private string fallfloortag = "FallFloor";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //設置判定
    public bool IsGround()
    {
        if(isGroundEnter || isGroundStay)
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
        if(collision.tag == groundTag || collision.tag == movefloorTag || collision.tag == fallfloortag)
        { 
             isGroundEnter = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == movefloorTag || collision.tag == fallfloortag)
        {
            isGroundStay = true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == movefloorTag || collision.tag == fallfloortag)
        {
            isGroundExit = true;
        }
    }
}
