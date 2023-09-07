using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownFloor : MonoBehaviour
{
    public GameObject spriteObj;
    public float vibrationWidt = 0.05f; //振動幅
    public float vibrationSpeed = 30.0f;//振動速度
    public float fallTime = 1.0f; //落ちる時間
    public float fallSpeed = 10.0f;//落ちる速度
    public float returnTime = 5.0f;//戻る速度
    private bool isOn;
    private bool isFall;
    private bool isReturn;
    private Vector3 spriteDefoultPos;
    private Vector3 floorDefaultPos;
    private Vector2 fallVelocity;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float timer = 0.0f;
    private float fallingTimer = 0.0f;
    private float returnTimer = 0.0f;
    private float blirkTimer = 0.0f;
    private string playertag = "Player";
    private bool player = false;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
        if(spriteObj != null && col != null && rb != null)
        {
            spriteDefoultPos = spriteObj.transform.position;
            fallVelocity = new Vector2(0, -fallSpeed);
            floorDefaultPos = gameObject.transform.position;
            sr = spriteObj.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            isOn = true;
            player = false;
        }
        if(isOn && !isFall)
        {
            float x = vibrationWidt * Mathf.Sin(vibrationSpeed * timer);
            spriteObj.transform.position = spriteDefoultPos + new Vector3(x, 0, 0);
            if(timer > fallTime)
            {
                isFall = true;
 
            }
            timer += Time.deltaTime;
        }
        if(isReturn)
        {
            if(blirkTimer > 0.2f)
            {
                sr.enabled = true;
                blirkTimer = 0.0f;
            }
            else if(blirkTimer > 0.1f)
            {
                sr.enabled = false;
            }
            else
            {
                sr.enabled = true;
            }
            if(returnTimer > 1.0f)
            {
                isReturn = false;
                blirkTimer = 0f;
                returnTime = 0f;
                sr.enabled = true;
            }
            else
            {
                blirkTimer += Time.deltaTime;
                returnTimer += Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        //落下中
        if(isFall)
        {
            rb.velocity = fallVelocity;
            //元の位置に戻る
            if(fallingTimer > fallTime)
            {
                isReturn = true;
                transform.position = floorDefaultPos;
                rb.velocity = Vector2.zero;
                isFall = false;
                timer = 0.0f;
                fallingTimer = 0.0f;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isOn = false;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == playertag)
        {
            player = true;
        }
    }
}
