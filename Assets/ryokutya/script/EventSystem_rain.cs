using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_rain : MonoBehaviour
{
    public playerCheck player;
    public Timer True;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.outPlayer)
        {
            Time.timeScale = 0;
            music.pitch = 0;
            Debug.Log("false");
        }
        if (True.limit)
        {
            Time.timeScale = 0;
            music.pitch = 0;
            Debug.Log("success");
        }

    }
}
