using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Dog dog;
    public float speed = 1;
    Vector2 dir;
    public float force;
    Class1 sex;
    bool left;
    public float rad = 540;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog").GetComponent<Dog>();
        sex = GameObject.Find("Sex").GetComponent<Class1>();
        dir = (dog.transform.position - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dir.x > 0 == transform.localScale.x > 0) transform.FlipX();
        if(sex.drawable) return;
        var dogDir = (dog.transform.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + speed * Time.fixedDeltaTime * dir);

        if(Vector2.Dot(dir,dogDir) < 0.9){
            dir = Quaternion.Euler(0, 0, (left?1:-1)*rad*Time.deltaTime)*dir;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        other.collider.GetComponent<Rigidbody2D>().AddForce(
            dir*force
        );
        dir *= -1;
        left = Random.value >= 0.5f;
    }
}
