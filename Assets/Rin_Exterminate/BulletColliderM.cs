using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColliderM : ColliderM
{
    public Vector2 pos,pre,dir;
    public float delta;
    public float r;

    public BulletColliderM(Action<CollisionM> onCollision,float r) : base(onCollision)
    {
        manager.bullets.Add(this);
        this.r = r;
    }

    public void jump(Vector2 pos){
        this.pos = pre = pos;
        this.delta = 0;
    }

    public void move(float delta){
        pre = pos;
        pos += delta*dir;
        this.delta = delta;
        manager.modifyBulletDelta(this);
    }

    public void rotate(float rad){
        this.dir = new(Mathf.Cos(rad),Mathf.Sin(rad));
    }

    public void flip(bool axisX,bool axisY){
        if(axisX) dir.y = -dir.y;
        if(axisY) dir.x = -dir.x;
    }

    public static void onCollision_Monst(BulletColliderM b,CollisionM c){
        float absDirX = Mathf.Abs(b.dir.x);
        if(c.normal.y > absDirX && b.dir.y < 0) b.flip(true,false);
        else if(c.normal.y < -absDirX && b.dir.y > 0) b.flip(true,false);
        else if(c.normal.x*b.dir.x < 0) b.flip(false,true);
    }
}
