using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RinExterminate;

public class WallBody : MonoBehaviour{
    void Start(){
        Vector2 center = this.transform.position;
        Vector2 size = this.transform.localScale*0.5f;
        var p = new Vector2[]{
            new (center.x - size.x,center.y + size.y),
            center + size,
            new (center.x + size.x,center.y - size.y),
            center - size
        };
        new LineColliderM(p[1],p[0],onCollision);
        new LineColliderM(p[2],p[1],onCollision);
        new LineColliderM(p[3],p[2],onCollision);
        new LineColliderM(p[0],p[3],onCollision);
    }

    void onCollision(CollisionM c){
        Manager.This.wally();
    }
}