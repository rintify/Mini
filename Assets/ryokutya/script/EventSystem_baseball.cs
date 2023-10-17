using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_baseball : MonoBehaviour
{
    public GameObject ball;
    public bat True1;
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
                Debug.Log("success");
            }
            else
            {
                baseball = false;
                Debug.Log("false");
            }
            Common.EndGame(baseball);
        });
    }

    // Update is called once per frame
    void Update()
    {
        ball.SetActive(true);
    }
}

