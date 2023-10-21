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
        public AudioClip sound1;
        public AudioSource source;
        public AudioClip sound2;
        public List<GameObject> livers = new();
        public MathEX.Virgin ending;
        // Start is called before the first frame update
        void Start()
        {
            Common.StartGame(13,()=>{
                Common.EndGame(false);
            });
            this.guidCollider = new BulletColliderM(c => BulletColliderM.onCollision_Monst(guidCollider,c),1f);
            guidCollider.exitst = false;
            ending = new(()=>{
                this.Delay(() => {
                    if(livers.Count == 0) Common.EndGame(true);
                    else Common.EndGame(false);
                }, 0.5f);
            });
        }

        public static Manager This{
            get{
                var manager = GameObject.Find("GameManager");
                return manager.GetComponent<Manager>();
            }
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

        public void kan(){
            source.PlayOneShot(sound1);
        }

        public void wally(){
            source.PlayOneShot(sound2);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.D)){
                taiho.transform.Rotate(0,0,-140f*Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A)){
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