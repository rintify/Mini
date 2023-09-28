 using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
using UnityEngine;

public class player_one : MonoBehaviour
{
    //
    [Header("")] public float speed;
    [Header("")] public float gravity;
    [Header("")] public float jumpSpeed;
    [Header("")] public float jumpHeight;
    [Header("")] public float jumpLimitTime;
    [Header("")] public GroundCheck ground;
    [Header("")] public GroundCheck head;
    [Header("")] public AnimationCurve dashCurve;
    [Header("")] public AnimationCurve jumpCurve;
    [Header("SE")] public AudioClip jumpSE;


    //private
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;
    private float beforeKey = 0.0f;

    // Start is called before the first frame update 
    void Start()
    {
        //
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //
        isGround = ground.IsGround();
        isHead =head.IsGround();

        //
        float horizontalKey = Input.GetAxis("Horizontal");
        bool verticalKey = Input.GetKey(KeyCode.Space);

        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        //Debug.Log(isGround);
        if (isGround)
        {
            if(verticalKey)
            {
                if (!isJump)
                {
                    GameManager.instance.PlaySE(jumpSE); 
                }
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //
            bool pushUpKey = verticalKey ;
            //
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime += 0.0f;
            }
        }

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("run", false);
            dashTime = 0.0f;
            xSpeed = 0.0f;
        }

        //
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        //
        xSpeed *= dashCurve.Evaluate(dashTime);
        if(isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}
