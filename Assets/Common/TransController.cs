using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransController : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = $"Score: {Common.Score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
