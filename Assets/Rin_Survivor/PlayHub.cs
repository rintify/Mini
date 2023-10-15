using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHub : Hub
{
    public float moveSpeed = 5.0f;
    private AudioSource source;
    public AudioClip kakin;
    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(13,()=>{
            Common.EndGame(true);
        });
        source = GetComponent<AudioSource>();
    }

    public void kan(){
        source.PlayOneShot(kakin);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // ←または→キーが押されたときの値
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized; // 正規化して斜め移動の速度を等しくする
        transform.position += moveSpeed*Time.deltaTime*moveDirection;
        /*if(transform.position.x > 10) transform.position = transform.position.X(10);
        else if(transform.position.x < -10) transform.position = transform.position.X(-10);
        if(transform.position.y > 5) transform.position = transform.position.Y(5);
        else if(transform.position.y < -5) transform.position = transform.position.Y(-5);*/
    }

    private void OnDestroy() {
        Common.EndGame(false);
    }
}
