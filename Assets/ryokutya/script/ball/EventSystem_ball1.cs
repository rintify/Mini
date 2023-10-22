using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_ball1 : MonoBehaviour
{
    public ball Ball1;
    public ball Ball2;
    public AudioSource music;
    private bool isGame = true;

    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () =>
        {
            if(isGame)
            {
                isGame = false;
                Debug.Log("success");
                Common.EndGame(true);
            }
        });
     }

    // Update is called once per frame
        void Update()
    {
        if(isGame)
        {
            if (Ball1.On || Ball2.On)
            {
                isGame = false;
                Debug.Log("false");
                Common.EndGame(false);
            }
        }
    }
}
