using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_baseball : MonoBehaviour
{
    public bat True1;
    public Timer False1;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (False1.limit)
        {
            if(True1.ball)
            {
                Time.timeScale = 0;
                music.pitch = 0;
                Debug.Log("success");
            }
            else
            {
                Time.timeScale = 0;
                music.pitch = 0;
                Debug.Log("false");
            }
        }
    }

}

