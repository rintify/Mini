using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public int enemy = 6;
    //new Vector3 point;
    public GameObject target;
    public float speed = 3f;
    private float player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
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
        if(Input.GetKey(KeyCode.LeftArrow)){
            player = -speed;

        } else if(Input.GetKey(KeyCode.RightArrow)){
            player = speed;
        }else {
            player = 0;
        }
        rb.velocity = new Vector2(player,rb.velocity.y);
    }
}
