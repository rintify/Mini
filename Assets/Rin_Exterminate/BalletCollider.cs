using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletCollider
{
    public Action<ICollision> onCollision;
    public Vector2 pos,pre,dir;
    public float r;

    public BalletCollider(Action<ICollision> onCollision){
        this.onCollision = onCollision;
    }

    public void jump(Vector2 pos){
        this.pos = pos;
        pre = pos;
    }

    public void move(float delta){
        pre = pos;
        pos += delta*dir;
    }

    public void flip(bool axisX,bool axisY){
        if(axisX) pos.y = -pos.y;
        if(axisY) pos.x = -pos.x;
    }
}
