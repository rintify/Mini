using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ : MonoBehaviour
{
    public float speed;//速度
    public float size;//playerの大きさ
    public Groundcheck ground;//接地判定
    public Groundcheck head;//頭判定
    //プライベート変数
    private CapsuleCollider2D capcol = null;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//Animaterの取得
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
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-size, size, 1);
            anim.SetBool("run", true);
            xSpeed = -speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(size, size, 1);
            xSpeed = speed;
        }
        else
        {
            anim.SetBool("run", false);
            xSpeed = 0.0f;
        }
        anim.SetBool("ground", isGround);
        rb.velocity = new Vector2(xSpeed,0);
    }
}
