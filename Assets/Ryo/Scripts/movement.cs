using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public static int enemy = 8;
    //new Vector3 point;
    public GameObject target;
    public float speed = 3f;
    private float player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if(Common.RequiredLevel==1){
            enemy=2;
        }else if(Common.RequiredLevel==2)
        {
            enemy=4;
        }else if(Common.RequiredLevel==3){
            enemy=6;
        }
        rb= GetComponent<Rigidbody2D>();
        for (int i = 0; i < enemy; i++)
        {
            //Debug.Log("Entering Main");
            //point = new Vector3(Random.Range(-9f, 9f), 3.5f, 0f);
            Instantiate(target, new Vector3(Random.Range(-9f, 9f), 3.5f, 0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        {
            player = -speed;

        } else if(Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            player = speed;
        }else {
            player = 0;
        }
        rb.velocity = new Vector2(player,rb.velocity.y);
    }
}
