using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleMusic : MonoBehaviour
{
    public float vx = 5;
    float vy = 0;
    bool gameover = false;
    public float gravity = 10f,power = 20f,vymax = 3f;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover) return;
        var pos = transform.position;

        vy -= gravity*Time.deltaTime;
        if(vy < -vymax) vy = - vymax;
        if(Input.GetKey(KeyCode.Space)){
            vy += power*Time.deltaTime;
            if(vy > vymax) vy = vymax;
        }

        pos.y += vy*Time.deltaTime;
        pos.x += vx*Time.deltaTime;

        transform.position = pos;

        camera.transform.position =
        camera.transform.position.X(pos.x);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("gameover");
        gameover = true;
    }

    
}
