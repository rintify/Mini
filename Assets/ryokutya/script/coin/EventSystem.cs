using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public coins True;
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
                Debug.Log("false");
                Common.EndGame(false);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(isGame)
        {
            if (True.clear)
            {
                isGame = false;
                Debug.Log("success");
                Common.EndGame(true);
            }
        }
    }
}
