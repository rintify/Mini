using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TestTimer : MonoBehaviour
{
    public bool limit = false;
    public float CountDownTime;
    public Text Texttime;
    public Image im;
    public float flashspeed;
    private float time;
    private float frequent = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = CountDownTime = Common.TimeLimit;
        CountDownTime += 0.5f; //カウントダウン開始まで0.5秒待機する
    }

    // Update is called once per frame
    void Update()
    {
        //カウントダウン開始まで0.5秒待機するための処理
        if(CountDownTime > time){
            CountDownTime -= Time.deltaTime;
            return;
        }
        if (CountDownTime > 0)
        {
            CountDownTime -= Time.deltaTime;
            im.fillAmount = CountDownTime / time;
            if (CountDownTime < 4)
            {
                Texttime.color = GetAlphaColor(Texttime.color);
                //Texttime.color = Color.red;
                Texttime.text = Mathf.CeilToInt(CountDownTime - 1).ToString();
            }
        }
        else
        {
            Texttime.text = "0";
            Common.TimeUp();
            this.gameObject.SetActive(false);
            limit = true;
        }

        Color GetAlphaColor_(Color color)
        {
            frequent += Time.deltaTime * 5.0f * flashspeed;
            color = Color.red;
            color.a = Mathf.Sin(frequent) * 0.5f + 0.5f;

            return color;
        }
        Color GetAlphaColor(Color color)
        {
            color = Color.red;
            color.a = -Mathf.Cos((time-CountDownTime)*Mathf.PI*2) * 0.5f + 0.5f;

            return color;
        }
    }
}
