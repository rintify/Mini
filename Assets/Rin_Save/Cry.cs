using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cry : MonoBehaviour
{
    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        this.Delay(()=>Destroy(this.gameObject),0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        speed -=   0.01f*Time.deltaTime*Vector2.up;
        transform.position += (Vector3)speed;
        transform.rotation = speed.normalized.Quaternion();
    }
}
