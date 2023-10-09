using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Dog dog;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog").GetComponent<Dog>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (dog.transform.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * dir;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        speed = 0;
    }
}
