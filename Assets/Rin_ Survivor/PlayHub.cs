using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHub : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // ←または→キーが押されたときの値
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized; // 正規化して斜め移動の速度を等しくする
        transform.position += moveSpeed*Time.deltaTime*moveDirection;
    }
}
