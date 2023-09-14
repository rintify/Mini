using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
   
    GameObject squere;
    count count;
    // Start is called before the first frame update
    void Start()
    {
        squere = GameObject.Find("Squere");
        count = squere.GetComponent<count>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        count.number +=1;
        GameObject.Destroy(this.gameObject);
    }
}
