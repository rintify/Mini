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
        Common.StartGame(5, () => {
            if (True1.ball)
            {
                baseball = true;
                Time.timeScale = 0;
                music.pitch = 0;
                Debug.Log("success");
                Common.EndGame(true);
            }
            else
            {
                baseball = false;
                Time.timeScale = 0;
                music.pitch = 0;
                Debug.Log("false");
                Common.EndGame(false);
            }
            Common.EndGame(baseball);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(sub.apper)
        {
            ball.SetActive(true);
            sub.apper = false;
        }
    }
}

