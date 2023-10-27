using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orgasm : MonoBehaviour
{
    [NonSerialized]
    public Hub parent;
    private PlayHub play;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<Hub>();
        play = transform.parent.GetComponent<PlayHub>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(
            parent.transform.position, 
            Vector3.forward, 
            parent.rotationSpeed*Time.deltaTime
        );

        transform.LookAt(parent.transform.position);
        transform.Rotate(0, 90, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<PlayHub>() != null){
            Debug.Log("player");
            var play = other.GetComponent<PlayHub>();
            Common.PlayOneShot(play.duhu);
            Destroy(other.GetComponent<PlayHub>().gameObject);
            Common.IsCleared = false;
            this.Delay(()=>Common.EndGame(),0.4f);
        }
        else if(other.GetComponent<EnemyHub>() != null){
            Debug.Log("enemy");
            if(play) Common.PlayOneShot(play.duhu);
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<EnemyHubLv1>() != null){
            Debug.Log("enemy");
            if(play) Common.PlayOneShot(play.duhu);
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<Disaster>() != null){
            Debug.Log("enemy");
            if(play) Common.PlayOneShot(play.duhu);
            Destroy(other.gameObject);
        }
        else{
            parent.rotationSpeed *= -1;
            if(play) Common.PlayOneShot(play.kakin);
        }
    }

    public Vector2 Dir {get {return (transform.position - parent.transform.position).normalized;}}
}
