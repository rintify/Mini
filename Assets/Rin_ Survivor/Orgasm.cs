using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orgasm : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(
            center.transform.position, 
            Vector3.forward, 
            rotationSpeed*Time.deltaTime
        );

        transform.LookAt(center.transform.position);
        transform.Rotate(0, 90, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        rotationSpeed *= -1;
    }
}
