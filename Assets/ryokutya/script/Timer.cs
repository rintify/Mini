using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool limit = false;
    public float CountDownTime;
    public Text Texttime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CountDownTime > 0)
        {
            CountDownTime -= Time.deltaTime;
            Texttime.text = "じかん:" + CountDownTime.ToString("0.00");
        }
        else
        {
            limit = true;
            Texttime.text = "じかん:" + CountDownTime.ToString("0.00");
        }
    }
}
