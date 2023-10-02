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
        public Line l1,l2;
        BulletColliderM guidCollider;
        // Start is called before the first frame update
        void Start()
        {
            this.guidCollider = new BulletColliderM(c => BulletColliderM.onCollision_Monst(guidCollider,c),1f);
            guidCollider.exitst = false;
        }

        void guid(){
            guidCollider.r = bulletPrefab.transform.localScale.x*0.5f;
            guidCollider.jump(taiho.transform.position);
            guidCollider.dir = taiho.transform.eulerAngles.z.Deg2Direction();
            var points = new List<Vector2>();
            points.Add(guidCollider.pos);
            guidCollider.move(100f);
            points.Add(guidCollider.pos);
            guidCollider.move(100f);
            points.Add(guidCollider.pos);
            l1.set(points[0],points[1]);
            l2.set(points[1],points[2]);
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
                    taiho.transform.position,
                    taiho.transform.eulerAngles.z*Mathf.Deg2Rad,
                    speed
                );
            }
            guid();
        }
    }
}