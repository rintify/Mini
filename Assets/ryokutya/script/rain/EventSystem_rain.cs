using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_rain : MonoBehaviour
{
    public playerCheck player;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () => {
            {
                Debug.Log("success");
                Common.EndGame(true);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (player.outPlayer)
        {
            Debug.Log("false");
            Common.EndGame(false);
        }
    }
}
