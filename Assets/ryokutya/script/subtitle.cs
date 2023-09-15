using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class subtitle : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Initialize()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            Destroy(this.gameObject);
            Time.timeScale = 1;
        }
    }
}
