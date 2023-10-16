using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orgasm : MonoBehaviour
{
    private Hub parent;
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
            //Destroy(other.gameObject);
            Common.EndGame(false);
        }
        else if(other.GetComponent<EnemyHub>() != null){
            Debug.Log("enemy");
            Destroy(other.gameObject);
        }
        else{
            parent.rotationSpeed *= -1;
            if(play) play.kan();
        }
    }
}
