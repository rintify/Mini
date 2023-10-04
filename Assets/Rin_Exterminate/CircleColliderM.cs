using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderM : ColliderM
{
    public Vector2 pos,n0;
    public float r,n0_nf,n0_x_nf;

    public CircleColliderM(Vector2 pos,float r,float nRad0,float nRadf, Action<CollisionM> onCollision) : base(onCollision){
        this.pos = pos;
        this.r = r;
        this.n0 = new(Mathf.Cos(nRad0),Mathf.Sign(nRad0));
        n0_nf = 0; 
        manager.circles.Add(this);
    }
}
