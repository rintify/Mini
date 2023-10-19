using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem_paperplane : MonoBehaviour
{
    public paperplane False;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () =>
        {
            Debug.Log("success");
            Common.EndGame(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (False.touch)
        {
            Debug.Log("false");
            Common.EndGame(false);
        }
    }

}
