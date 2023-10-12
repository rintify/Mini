using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Counter : MonoBehaviour
{
    public Image image;
    Text text;
    public float count = 11;
    float current;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        current = count;
        /*this.Interval(()=>{
            text.text = count--.ToString();
            return count < 0;
        },1);*/
    }

    // Update is called once per frame
    void Update()
    {
            image.fillAmount = current/count;
            text.text = Mathf.CeilToInt(current-1).ToString();
            if(current < 4) text.color = Color.red;
            current -= Time.deltaTime;
            if(current < 0) this.gameObject.SetActive(false);
    }
}
