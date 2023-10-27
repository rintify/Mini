using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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
        [NonSerialized]
        public List<GameObject> livers = new();
        public EX.Virgin ending;
        public TextAsset json;
        public GameObject enemyPrefab;
        float rotSpeed = 0;
        // Start is called before the first frame update
        void Start()
        {
            var es = FindObjectsOfType<EnermyBody>();
            Debug.Log($"[\n{es.Select(e => $"\t[{e.transform.position.x},{e.transform.position.y}]").Join(",\n")}\n]");

            var stage = JsonConvert.DeserializeObject<float[][][][]>(json.text)
                [Common.RequiredLevel-1].ElementAtRandom();

            foreach(var s in stage){
                var a = Instantiate(enemyPrefab);
                a.transform.position = new(s[0],s[1]);
            }

            Common.StartGame(Common.RequiredLevel >= 3 ? 15 : 9,()=>{
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
            var slow = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 0.3f : 1f;
            if (Input.GetKey(KeyCode.D)){
                taiho.transform.Rotate(0,0,-slow*rotSpeed*Time.deltaTime);
                rotSpeed += (110f - rotSpeed)*6f*Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A)){
                taiho.transform.Rotate(0,0,slow*rotSpeed*Time.deltaTime);
                rotSpeed += (110f - rotSpeed)*6f*Time.deltaTime;
            }
            else rotSpeed = 0;

            if (Input.GetKeyDown(KeyCode.Space) && bullet == null){
                bullet = Instantiate(bulletPrefab);
                bullet.transform.position = taiho.transform.position;
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