using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class subtitle : MonoBehaviour
{
    public float time;
    public AudioSource music;
    public bool apper = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.unscaledDeltaTime;
        if(time <= 0)
        {
            apper = true;
            Destroy(this.gameObject);
            Time.timeScale = 1;
            music.pitch = 1;

        }
        else
        {
            Time.timeScale = 0;
            music.pitch = 0;

        }
    }
}
