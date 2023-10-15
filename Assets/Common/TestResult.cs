using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResult : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = $"Name: {Common.PlayerName}\nScore: {Common.Score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
