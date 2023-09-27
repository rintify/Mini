using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : ColliderM
{
    public Vector2 pos,delta,n;
    public float len;

    public WallCollider(Vector2 pos,Vector2 delta, Action<CollisionM> onCollision) : base(onCollision){
        this.pos = pos;
        this.delta = delta;
        len = delta.magnitude;
        n = new Vector2(-delta.y/len, delta.x/len);
    }
}
