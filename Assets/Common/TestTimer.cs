using System.Collections;
using System.Collections.Generic;
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
    private float speed = 2;
    private float frequent = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = CountDownTime = Common.TimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
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

        Color GetAlphaColor(Color color)
        {
            frequent += Time.deltaTime * 5.0f * flashspeed;
            color = Color.red;
            color.a = Mathf.Sin(frequent) * 0.5f + 0.5f;

            return color;
        }
    }
}
