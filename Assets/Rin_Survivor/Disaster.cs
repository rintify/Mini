using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    public PlayHub target;
    public DisasterBullet bulletPrefab; // 弾丸のPrefabを指定するためのパブリック変数
    public float bulletSpeed = 20f; // 弾丸の速度
    EX.Intervalist a,b;
    public float speed = 4;
    public float interval = 1;
    Vector3 prePos;
    int t = 0;
    float muki = 0;
    public List<DisasterBullet> refrectedBullets;

    void Start(){
        prePos = target.transform.position;

        b = new(()=>{
            muki = UnityEngine.Random.Range(0,3);
            muki = muki == 0 ? -1 : muki == 1 ? 0 : 1;
            Debug.Log(muki);
            a.interval -= interval*0.2f;
            if(a.interval < interval) a.interval = interval;
        },1.9f);

        a = new(() =>{
            Vector2 dir = 
                t%3 == 1 ? DirectionAfter() :
                t%3 == 2 ? (target.transform.position - (target.transform.position - prePos)*0.3f - transform.position).normalized :
                (target.transform.position - transform.position).normalized;
            if(t == 2){
                t = UnityEngine.Random.Range(0,2);
            }
            else t++;

            var bulletInstance = Instantiate(bulletPrefab); // Prefabをインスタンス化
            bulletInstance.transform.position = transform.position + (Vector3)(1.2f*dir);
            bulletInstance.transform.rotation = dir.Quaternion();
            bulletInstance.speed = bulletSpeed;
            bulletInstance.parent = this;

            prePos = target.transform.position;
        },interval*1.5f);
    }

    Vector2 DirectionAfter(){
        Vector2 p = target.transform.position - transform.position;
        Vector2 v = (target.transform.position - prePos)/a.interval*0.87f; 
        var s = bulletSpeed;
        var B = Vector2.Dot(p,v);
        var A = v.sqrMagnitude - s*s;
        var T = (-B - Mathf.Sqrt(B*B - A*p.sqrMagnitude))/A;
        Debug.Log(T);
        return (p/T + v)/s;
    }
    

    void Update()
    {
        if(!target) return;
        
        a.Update();
        b.Update();
        Vector2 pos = transform.position;
        Vector2 toTarget = target.transform.position - transform.position;
        var toTarget_ = toTarget.magnitude;
        foreach(var a in refrectedBullets){
            Vector2 aa = a.transform.position - transform.position;
            if(aa.sqrMagnitude < 4){
                Debug.Log(3312);
                pos -= speed*Time.deltaTime * aa.normalized;
                transform.position = pos;
                return;
            }
        }
        if(toTarget_ < 10){
            pos -= speed*Time.deltaTime * toTarget.normalized;
        }
        else if(toTarget_ < 10.5){
            pos += speed*Time.deltaTime * 
                muki*toTarget.normalized.Right();
        }
        else pos += speed*Time.deltaTime * toTarget.normalized;
        transform.position = pos;
    }
}
