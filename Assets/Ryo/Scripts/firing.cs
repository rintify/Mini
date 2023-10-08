using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class firing : MonoBehaviour
{
    public GameObject bullet;
    new Vector3 point;
    public int waittime = 50;
    int time =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+=1;
        if(Input.GetMouseButtonDown(0)|| Input.GetKeyDown("space"))
        {
            //Debug.Log(time);
            if(time>waittime){
            point = this.transform.position;
            Instantiate(bullet,point,Quaternion.identity);
            time =0;
            }
        }
    }
}
