using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHub : Hub
{
    PlayHub target;
    GameObject playerSword;
    public float speed = 4;
    GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<PlayHub>();
        playerSword = target.transform.Find("Sword").transform.Find("edge").gameObject;
        sword = transform.Find("Sword").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = target.transform.position - transform.position;
        Vector3 sd = playerSword.transform.position - transform.position;
        var s = sword.transform.position - transform.position;
        
        if(s.Cross2(d)*rotationSpeed < 0 && d.sqrMagnitude < 18){
            transform.position -= speed*Time.deltaTime*d.normalized;
        }
        else if(d.sqrMagnitude < 10){
            if(sd.sqrMagnitude < 5){
                transform.position -= speed*Time.deltaTime*(
                    (-Vector3.Cross(d,sd).z*d.Right()).normalized
                );
            }
            else{
                transform.position -= speed*Time.deltaTime*d.normalized;
            }
        }  
        else if(sd.sqrMagnitude < 3){
            transform.position -= speed*Time.deltaTime*d.normalized;
        }
        else if(d.sqrMagnitude > 12){
            transform.position += speed*Time.deltaTime*d.normalized;
        }
        else{
            transform.position += speed*Time.deltaTime*d.normalized.Right();
        }
    }
}
