using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isStop=false;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool verticalKey = Input.GetKey(KeyCode.Space);
        if (verticalKey)
        {
            Stop();
        }

        if (isStop) return ;
        //毎フレーム回転させる
        transform.Rotate(new Vector3(0, 0.5f, 0));

        //少しずつ下へ
        //transform.Translate(0, -0.005f, 0);

        //下まで行ったら上に戻る
        if (1 > transform.position.y)
        {
            Vector3 pos = transform.position;  
            pos.y = startY;
            transform.position = pos;
        }
    }

    //Colliderの当たり判定があった時に呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.name.Equals("Clear"))
        {
            Debug.Log("game3クリア!");
        }
    }

    public void Stop()
    {
        isStop = true;
        //落下させる
        GetComponent<Rigidbody>().isKinematic = false;
    }

    /*public void Retry()
    {
        SceneManager.LoadScene("game3");
    }
    */
}
