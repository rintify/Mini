using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResult2 : MonoBehaviour
{
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start() => text.text = $"Name: {Common.PlayerName}\nScore: {Common.Score}";

    // Update is called once per frame
    void Update()
    {

    }
}
