using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text_Life : MonoBehaviour
{
    public GameObject[] lifeArray = new GameObject[5];
    public float heart_xposition;
    public float heart_yposition;
    private int lifePoint;
    private RectTransform RectTransform_get;

    // Start is called before the first frame update
    void Start()
    {
        lifePoint = Common.Life;
        RectTransform_get = gameObject.GetComponent<RectTransform>();
        Vector2 pos = RectTransform_get.anchoredPosition;
        pos.x = heart_xposition;
        pos.y = heart_yposition;
        RectTransform_get.anchoredPosition = pos;
        if(lifePoint < 5)
        {
            for (int i = lifePoint; i < 5; i++)
            {
                lifeArray[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
