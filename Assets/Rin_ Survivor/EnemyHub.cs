using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHub : Hub
{
    public PlayHub target;
    public GameObject playerSword;
    public float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = target.transform.position - transform.position;
        Vector3 sd = playerSword.transform.position - transform.position;
        if(d.sqrMagnitude < 6){
            transform.position -= speed*Time.deltaTime*d.normalized;
        }  
        else if(sd.sqrMagnitude < 3){
            transform.position -= speed*Time.deltaTime*sd.normalized;
        }
        /*else if(d.sqrMagnitude > 9){
            transform.position += speed*Time.deltaTime*d.normalized;
        }  */
    }
}
