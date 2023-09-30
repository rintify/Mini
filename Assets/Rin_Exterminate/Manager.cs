using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RinExterminate{
    public class Manager : MonoBehaviour
    {
        public GameObject taiho;
        public BulletBody bulletPrefab;
        BulletBody bullet;
        public float speed = 300;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow)){
                taiho.transform.Rotate(0,0,-140f*Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftArrow)){
                taiho.transform.Rotate(0,0,140f*Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Space) && bullet == null){
                bullet = Instantiate(bulletPrefab);
                bullet.set(
                    taiho.transform.position + taiho.transform.forward*0f,
                    taiho.transform.eulerAngles.z/180*Mathf.PI,
                    speed
                );
            }
        }
    }
}