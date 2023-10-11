using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject prehabBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            //離されたマウスの場所へレイ(光線)を飛ばす
            Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);

            //ベクトルを取得(ワールド座標)
            Vector3 dir = ray.direction;

            GameObject bullet = Instantiate(prehabBullet);

            //発射する
            bullet.transform.position = transform.position;
            bullet.GetComponent<BulletController>().Shoot(dir.normalized*3000);
        }
    }
}
