using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletBody : MonoBehaviour
{
    BulletColliderM collider;
    float speed = 10;

    void Start(){
        collider = new BulletColliderM(onCollision,base.transform.localScale.x*0.5f);
        collider.jump(base.transform.position);
        collider.rotate(0);
    }

    void Update(){
        base.transform.position = collider.pos;
    }

    void FixedUpdate(){
        collider.move(speed);
        speed -= 1;
        if(speed < 0) speed = 0;
    }

    void onCollision(CollisionM c){
        float absDirX = Mathf.Abs(collider.dir.x);
        if(c.normal.y > absDirX || c.normal.y < -absDirX) collider.flip(false,true);
        else collider.flip(true,false);
    }
}
