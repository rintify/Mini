using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TestSubtitle : MonoBehaviour
{
    public float time;
    public bool apper = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GetComponent<TextMeshProUGUI>().text = Common.Title;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.unscaledDeltaTime;
        if(time <= 0)
        {
            apper = true;
            Destroy(this.gameObject);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
