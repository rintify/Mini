using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletBody : MonoBehaviour
{
    BulletColliderM collider;
    private float v,sqrt_speed;
    private float t;

    void Awake(){
        collider = new BulletColliderM(onCollision,base.transform.localScale.x*0.5f);
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
        t += 0.05f;
        if(v < 0) v = 0;
    }

    void onCollision(CollisionM c){
        float absDirX = Mathf.Abs(collider.dir.x);
        if(c.normal.y > absDirX && collider.dir.y < 0) collider.flip(true,false);
        else if(c.normal.y < -absDirX && collider.dir.y > 0) collider.flip(true,false);
        else if(c.normal.x*collider.dir.x < 0) collider.flip(false,true);
    }
}
