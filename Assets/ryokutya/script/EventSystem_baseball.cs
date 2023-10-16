using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_baseball : MonoBehaviour
{
    public GameObject ball;
    public bat True1;
    public Timer False1;
    public AudioSource music;
    public subtitle sub;
    private bool baseball;

    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(4, () => {Common.EndGame(baseball);});
    }

    // Update is called once per frame
    void Update()
    {
        if(sub.apper)
        {
            ball.SetActive(true);
            sub.apper = false;
        }
        if (False1.limit)
        {
            if(True1.ball)
            {
                baseball = true;
                Time.timeScale = 0;
                music.pitch = 0;
                Common.EndGame(true);
                Debug.Log("success");
            }
            else
            {
                baseball = false;
                Time.timeScale = 0;
                music.pitch = 0;
                Common.EndGame(false);
                Debug.Log("false");
            }
        }
    }

}

