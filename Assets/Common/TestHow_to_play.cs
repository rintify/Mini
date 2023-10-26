using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestHow_to_play : MonoBehaviour
{
    public float xposition;
    public float yposition;
    public float target_xposition;
    public float target_yposition;
    public float font_size;
    public float speed_rate;
    public float font_speed_rate;
    private float xspeed;
    private float yspeed;
    private bool pivot = false;

    //public  Timer sub;
    RectTransform RectTransform_get;
    public TestSubtitle subtitle;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Common.Instruction;
        RectTransform_get = gameObject.GetComponent<RectTransform>();
        Vector2 pos = RectTransform_get.anchoredPosition;
        pos.x = xposition;
        pos.y = yposition;
        RectTransform_get.anchoredPosition = pos;
        Vector2 scale = RectTransform_get.localScale;
        scale.x = 1.6f;
        scale.y = 1.6f;
        RectTransform_get.localScale = scale;
        xspeed = -xposition + target_xposition;
        yspeed = -yposition + target_yposition;
        pivot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //サブタイトルが左に行き始める時間まで待機
        if(!subtitle.apper) return;
        if(Time.timeScale == 0)
        {
            RectTransform_get = gameObject.GetComponent<RectTransform>();
            Vector2 pos = RectTransform_get.anchoredPosition;
            pos.x = xposition;
            pos.y = yposition;
            RectTransform_get.anchoredPosition = pos;
            Vector2 scale = RectTransform_get.localScale;
            scale.x = 1.6f;
            scale.y = 1.6f;
            RectTransform_get.localScale = scale;
            xspeed = -xposition + target_xposition;
            yspeed = -yposition + target_yposition;
        }
        else
        {
            Vector2 pos = RectTransform_get.anchoredPosition;
            Vector2 scale = RectTransform_get.localScale;
            if (pos.x > target_xposition)
            {
                pos.x += xspeed / speed_rate;
                pos.y += yspeed / speed_rate;
                scale.x -= font_size / font_speed_rate;
                scale.y -= font_size / font_speed_rate;
            }
            else
            {
                if (pivot)
                {
                    Vector2 target_pivot = new Vector2(0.0f, 0.0f);
                    RectTransform_get.pivot = target_pivot;
                    pivot = false;
                }
                pos.x = target_xposition;
                pos.y = target_yposition;
                scale.x = font_size;
                scale.y = font_size;
                //pos.x += xspeed / speed_rate;
                //pos.y += yspeed / speed_rate;
                //scale.x -= font_size/speed_rate;
                //scale.y -= font_size/speed_rate;
            }
            RectTransform_get.anchoredPosition = pos;
            RectTransform_get.localScale = scale;
        }
    }
}
