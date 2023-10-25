using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransController : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public TMPro.TMP_Text text1;
    void Start()
    {
        text.text = $"スコア: {Common.Score}";
        text1.text = $"レベル: {Common.Level}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
