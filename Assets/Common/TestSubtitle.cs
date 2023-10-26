using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TestSubtitle : MonoBehaviour
{
    public float time;
    public bool apper = false;
    public float subtarget_xposition;
    public float subtarget_yposition;
    public float subspeed_rate;
    public float subfont_size;
    public float subfont_speed_rate;
    private float subxposition;
    private float subyposition;
    private float subxspeed;
    private float subyspeed;
    private bool pivot = false;

    //public  Timer sub;
    RectTransform RectTransform_get;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GetComponent<TextMeshProUGUI>().text = Common.Title;
        RectTransform_get = gameObject.GetComponent<RectTransform>();
        Vector2 pos = RectTransform_get.anchoredPosition;
        subxposition = pos.x;
        subyposition = pos.y;
        subxspeed = -subyposition + subtarget_xposition;
        subyspeed = -subyposition + subtarget_yposition;
        Vector2 scale = RectTransform_get.localScale;
        scale.x = 1.0f;
        scale.y = 1.0f;
        RectTransform_get.localScale = scale;
        pivot = true;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.unscaledDeltaTime;
        if(time <= 0)
        {
            apper = true;
            //Destroy(this.gameObject);
            Time.timeScale = 1;

            Vector2 pos = RectTransform_get.anchoredPosition;
            Vector2 scale = RectTransform_get.localScale;
            if (pos.x > subtarget_xposition)
            {
                pos.x += subxspeed / subspeed_rate;
                pos.y += subyspeed / subspeed_rate;
                scale.x -= subfont_size / subfont_speed_rate;
                scale.y -= subfont_size / subfont_speed_rate;
            }
            else
            {
                if (pivot)
                {
                    Vector2 target_pivot = new Vector2(0.0f, 0.0f);
                    RectTransform_get.pivot = target_pivot;
                    pivot = false;
                }
                pos.x = subtarget_xposition;
                pos.y = subtarget_yposition;
                scale.x = subfont_size;
                scale.y = subfont_size;
                //pos.x += xspeed / speed_rate;
                //pos.y += yspeed / speed_rate;
                //scale.x -= font_size/speed_rate;
                //scale.y -= font_size/speed_rate;
            }
            RectTransform_get.anchoredPosition = pos;
            RectTransform_get.localScale = scale;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
