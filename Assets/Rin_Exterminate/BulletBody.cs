using System;
using System.Collections;
using System.Collections.Generic;
using RinExterminate;
using UnityEngine;

public class BulletBody : MonoBehaviour
{
    BulletColliderM collider;
    private float v,sqrt_speed;
    private float t;

    void Awake(){
        collider = new BulletColliderM(c => BulletColliderM.onCollision_Monst(collider,c),base.transform.localScale.x*0.5f);
    }

    public void set(Vector2 pos,float rad, float speed){
        collider.jump(pos);
        collider.rotate(rad);
        sqrt_speed = Mathf.Sqrt(speed);
        v = sqrt_speed*0.03f;
    }

    void Update(){
        base.transform.position = collider.pos;
    }

    void FixedUpdate(){
        collider.move(v);
        float a = t/sqrt_speed;
        v -= 1f*v*a*a*a*a;
        t += 0.1f;
        if(v < 0.001){
            v = 0;
            Manager.This.ending.Break();
        };
    }
}
