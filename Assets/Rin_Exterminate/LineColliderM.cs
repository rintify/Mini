using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColliderM : ColliderM
{
    public Vector2 pos,delta,n;
    public float len;

    public LineColliderM(Vector2 pos0,Vector2 posf, Action<CollisionM> onCollision) : base(onCollision){
        this.pos = pos0;
        this.delta = posf - pos0;
        len = delta.magnitude;
        n = new(-delta.y/len, delta.x/len);
        manager.lines.Add(this);
    }
}
