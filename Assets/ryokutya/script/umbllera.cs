using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class umbllera : MonoBehaviour
{
    public float Speed;
    List<int> list = new List<int>();
    private float time;
    private int x;
    private int y;
    private Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        list.Add(-1);
        list.Add(1);
        x = Random.Range(0, 2);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1)
        {
            time = 0;
            x = Random.Range(0, 2);
            y = 1 - x;
        }
        rb.velocity = new Vector2(Speed * list[x], 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "wall")
        {
            x = y;
            rb.velocity = new Vector2(Speed * list[x], 0);
        }
    }

}
