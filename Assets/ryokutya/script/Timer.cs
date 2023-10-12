using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool limit = false;
    public float CountDownTime;
    public Text Texttime;
    public Image im;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = CountDownTime;
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
                Texttime.color = Color.red;
                Texttime.text = Mathf.CeilToInt(CountDownTime - 1).ToString();
            }
        }
        else
        {
            Texttime.text = "0";
            this.gameObject.SetActive(false);
            limit = true;
        }
    }
}
