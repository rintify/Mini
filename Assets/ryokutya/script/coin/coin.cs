using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public coins Many;
    public bool SE = false;
    private bool isOn = false;
    private string playerTag = "Player";


    // Start is called before the first frame update
    void Start()
    {

     }

    // Update is called once per frame
    void Update()
    {
      if(isOn)
      {
            Destroy(this.gameObject);
            Many.many += 1;
       }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn = true;
            SE = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn = false;
        }
    }
}
