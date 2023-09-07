using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider
{
    public Action<ICollision> onCollision;
    public Vector2 pos,n0;
    public float r,n0_nf,n0_x_nf;

    public BallCollider(Vector2 pos,float r,float nRad0,float nRadf,Action<ICollision> onCollision){
        this.onCollision = onCollision;
        n0_nf = 0; 
    }
}
