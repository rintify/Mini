using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    //カウントダウン
    public float countdown ;

    //終わったら止める
    private float stop =1.0f;

    //時間を表示するText型の変数
    public Text timeText;

    // Update is called once per frame
    void Update()
    {

        //時間をカウントする
        countdown -= Time.deltaTime * stop;

        

        if (countdown <= 0)
        {
            timeText.text = "時間になりました！";
            stop = 0;
            countdown = 8.0f;
        }
        else
        {
            //時間を表示する
            timeText.text = countdown.ToString("F1") + "秒";
        }
    }
}