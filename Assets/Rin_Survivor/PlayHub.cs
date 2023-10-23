using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHub : Hub
{
    public float moveSpeed = 5.0f;
    public AudioClip kakin,duhu,ban;
    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(13,()=>{
            Common.EndGame(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // ←または→キーが押されたときの値
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized; // 正規化して斜め移動の速度を等しくする
        transform.position += moveSpeed*Time.deltaTime*moveDirection;

        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new(
            Mathf.Clamp(transform.position.x, -screenBounds.x*1.1f, screenBounds.x*1.1f),
            Mathf.Clamp(transform.position.y, -screenBounds.y*1.1f, screenBounds.y*1.1f)
        );
        /*if(transform.position.x > 10) transform.position = transform.position.X(10);
        else if(transform.position.x < -10) transform.position = transform.position.X(-10);
        if(transform.position.y > 5) transform.position = transform.position.Y(5);
        else if(transform.position.y < -5) transform.position = transform.position.Y(-5);*/
    }
}
