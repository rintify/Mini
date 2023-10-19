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
    MathEX.Intervalist inter;
    Vector2 preT, prepreT;
    float rotationTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<PlayHub>();
        playerSword = target.transform.Find("Sword").transform.Find("edge").gameObject;
        sword = transform.Find("Sword").gameObject;

        inter = new(() => {
            Vector2 T = target.transform.position - transform.position;
            if(Vector2.Dot(T,preT) > 0.7 && Vector2.Dot(T,prepreT) > 0.7){
                rotationTime = 0.7f;
            }
            prepreT = preT;
            preT = T;
        }, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        inter.Update();

        Vector2 pos = transform.position;

        Vector2 toTarget = target.transform.position - transform.position;
        var toTarget_ = toTarget.magnitude;
        Vector2 toPSword = playerSword.transform.position - transform.position;

        Vector2 pSword = (playerSword.transform.position - target.transform.position).normalized;
        Vector2 sword = (this.sword.transform.position - transform.position).normalized;

        if(rotationTime > 0){
            if(toTarget_ < 4.2){
                pos -= speed*Time.deltaTime * toTarget.normalized;
            }  
            else if(toTarget_ < 4.3){
                pos += speed*Time.deltaTime * 
                    Mathf.Sign(target.rotationSpeed) * toTarget.normalized.Right();
            }
            else{
                pos += speed*Time.deltaTime * toTarget.normalized.Rotate(
                    -Mathf.Sign(target.rotationSpeed)*1.1f
                );
            } 
            rotationTime -= Time.deltaTime;
            transform.position = pos;
            return;
        }

        var T_o_PS= Vector2.Dot(toTarget.normalized,pSword);
        var T_x_PS = toTarget.normalized.Cross(pSword);
        
        if(toTarget_ < 3.16){
            pos -= speed*Time.deltaTime * toTarget.normalized;
        }  
        else if(
            !(toTarget.normalized.Dot(sword) > -1 &&
            toTarget.normalized.Cross(sword) * -Mathf.Sign(rotationSpeed) > -0.1)|| 
            T_x_PS * Mathf.Sign(target.rotationSpeed) > 0
        ){
            if(toTarget_ < 4.2){
                pos -= speed*Time.deltaTime * toTarget.normalized;
            }
            else if(toTarget_ < 4.3){
                pos += speed*Time.deltaTime * 
                    Mathf.Sign(target.rotationSpeed) * toTarget.normalized.Right()*0.1f;
            }
            else{
                pos += speed*Time.deltaTime * toTarget.normalized;
            }
        }
        else if(toTarget_ < 3.46){
            pos += speed*Time.deltaTime * 
                    Mathf.Sign(target.rotationSpeed) * toTarget.normalized.Right();
        }
        else{
            pos += speed*Time.deltaTime * toTarget.normalized;
        }

        transform.position = pos;
    }
}
