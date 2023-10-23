using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHubLv1 : Hub
{
    PlayHub target;
    public float speed = 4;
    List<EnemyHubLv1> enemys;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player")?.GetComponent<PlayHub>();
        enemys = GameObject.FindObjectsOfType<EnemyHubLv1>().ToList();
        enemys.Remove(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!target) return;
        
        Vector2 pos = transform.position;

        Vector2 toTarget = target.transform.position - transform.position;
        var toTarget_ = toTarget.magnitude;

        foreach(var e in enemys){
            if(!e) continue;
            Vector2 aa = e.transform.position - transform.position;
            if(aa.sqrMagnitude < 4.2*4.2){
                pos -= speed*Time.deltaTime * aa.normalized;
                transform.position = pos;
                return;
            }
        }
        if(toTarget_ < 3.16){
            pos -= speed*Time.deltaTime * toTarget.normalized;
        }  
        else if(toTarget_ < 3.46){
            pos += speed*Time.deltaTime * 
                     toTarget.normalized.Right();
        }
        else{
            pos += speed*Time.deltaTime * toTarget.normalized;
        }

        transform.position = pos;
    }
}
