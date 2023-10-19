using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public coins True;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () =>
        {
            Debug.Log("false");
            Common.EndGame(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(True.clear)
        {
            Debug.Log("success");
            Common.EndGame(true);
        }
    }
}
