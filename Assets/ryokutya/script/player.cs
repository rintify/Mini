using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;//速度
    public float jumpSpeed;//ジャンプ速度
    public float jumpacc;//ジャンプ加速度
    public float jumpHeight;//ジャンプの高さ
    public float gravity;//重力
    public float jumpLimitTime;//ジャンプの制限
    public float stepOnRate;//踏みつけ判定の高さの割合
    public float size;//playerの大きさ
    public Groundcheck ground;//接地判定
    public Groundcheck head;//頭判定
    //プライベート変数
    private CapsuleCollider2D capcol = null;
    private Moveobject moveObj = null;
    //private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private bool Space = true;
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();//Animaterの取得
        rb = GetComponent<Rigidbody2D>();  //Rigidbody2Dの取得
        capcol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //接地判定を得る
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //キー移動
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-size, size, 1);
            //anim.SetBool("run", true);
            xSpeed = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //anim.SetBool("run", true);
            transform.localScale = new Vector3(size, size, 1);
            xSpeed = speed;
        }
        else
        {
            //anim.SetBool("run", false);
            xSpeed = 0.0f;
        }

        //ジャンプ
        if (isGround)
        {
          if (Input.GetKey(KeyCode.Space) && Space)
           {
                Space = false;
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
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;
            if (Input.GetKey(KeyCode.Space) && canHeight && canTime && !isHead && !Space)
            {
                ySpeed = -jumpacc * jumpTime + jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else if (!Input.GetKey(KeyCode.Space))
            {
                Space = true;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            Space = true;
        }
        Vector2 addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        //anim.SetBool("jump", isJump);
        rb.velocity = new Vector2(xSpeed, ySpeed) + addVelocity;
    }
}
