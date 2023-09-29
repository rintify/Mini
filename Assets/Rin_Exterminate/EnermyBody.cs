using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        new CircleColliderM(base.transform.position,base.transform.localScale.x*0.5f,0,Mathf.PI*2,onCollision);
    }

    void onCollision(CollisionM c){

    }
}
