using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : ColliderM
{
    public Vector2 pos,pre,dir;
    public float delta;
    public float r;

    public BulletCollider(Action<CollisionM> onCollision) : base(onCollision)
    {
    }

    public void jump(Vector2 pos){
        this.pos = pos;
        pre = pos;
        this.delta = 0;
    }

    public void move(float delta){
        pre = pos;
        pos += delta*dir;
        this.delta = delta;
    }

    public void flip(bool axisX,bool axisY){
        if(axisX) pos.y = -pos.y;
        if(axisY) pos.x = -pos.x;
    }
}
