using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public void set(Vector2 p,Vector2 q){
        this.transform.localPosition = (q + p)*0.5f;
        Vector2 d = q - p;
        this.transform.localEulerAngles = 
            this.transform.localEulerAngles.Z(Mathf.Atan2(d.y,d.x)*Mathf.Rad2Deg);
        this.transform.localScale = this.transform.localScale.X(d.magnitude);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
