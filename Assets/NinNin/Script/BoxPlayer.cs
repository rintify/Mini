using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayer : MonoBehaviour
{
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x > -5)
                this.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x < 5)
                this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
