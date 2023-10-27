using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
       

        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.W))
        {
            
            pos.y-=0.01f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.S))
        {
            pos.y+=0.01f;
        }
        myTransform.position = pos;  // 座標を設定
    }
}
