using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanter : MonoBehaviour
{
    Transform player;
    EX.Intervalist ptrace;
    List<Vector3> poss = new();
    Rigidbody rb;
    public float speed;
    Door g;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        ptrace = new(() => {
            if(poss.Count == 0 || (poss[^1] - player.position).magnitude > 1)
                poss.Add(player.position);
        },0.5f,2f);
         g = transform.parent.parent.Find("Door").GetComponent<Door>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!g.isOpen) return;
        ptrace.Update();
        if(poss.Count >= 1){
            var d = poss[0]-transform.position;
            if(d.magnitude < 0.07) poss.RemoveAt(0);
            transform.position += speed*Time.fixedDeltaTime*d.normalized;
        }
        
    }
}
