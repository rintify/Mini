using System.Collections;
using System.Collections.Generic;
using RinExterminate;
using UnityEngine;

public class EnermyBody : MonoBehaviour
{
    CircleColliderM col;
    // Start is called before the first frame update
    void Start()
    {
        col = new CircleColliderM(base.transform.position,base.transform.localScale.x*0.5f,0,Mathf.PI*2,onCollision);
    }

    void Update(){

    }

    void onCollision(CollisionM c){
        Debug.Log("fa");
        col.exitst = false;
        Destroy(this.gameObject);
        Manager.This.kan();
    }
}
