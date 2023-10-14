using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
   
    public GameObject bullet;
    //count count;
    // Start is called before the first frame update
    void Start()
    {
        //squere = GameObject.Find("Squere");
        //count = squere.GetComponent<count>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.name == "bullet(Clone)"){
            count.number++;
        GameObject.Destroy(this.gameObject);
        }
    }
    
}
