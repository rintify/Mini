using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
   
    //public GameObject squere;
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
        //count.number++;
        Debug.Log("Hit"); // ログを表示する
        GameObject.Destroy(this.gameObject);
    }
    
}
