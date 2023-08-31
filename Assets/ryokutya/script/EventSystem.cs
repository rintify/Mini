using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Timer False;
    public coins True;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(False.limit)
        {
            Time.timeScale = 0;
            Debug.Log("false");
        }
        if(True.clear)
        {
            Time.timeScale = 0;
            Debug.Log("success");
        }
    }
}
