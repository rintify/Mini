using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentRank : MonoBehaviour
{
    RectTransform rt;
    Vector2 targetPos,startPos;
    Action state;
    public float up,speed;
    int rank;
    public AudioClip zyan;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        targetPos = rt.anchoredPosition;
        rt.anchoredPosition += Vector2.up*up;
        startPos = rt.anchoredPosition;

        state = () => {
            rt.anchoredPosition -= up*speed*Time.deltaTime*Vector2.up;
            if(rt.anchoredPosition.y < targetPos.y){
                if(rank == 1) Common.PlayOneShot(zyan);
                state = () => {};
                this.Delay(() => {
                    state = () => {
                        rt.anchoredPosition += up*speed*Time.deltaTime*Vector2.up;
                        if(rt.anchoredPosition.y > startPos.y) Destroy(gameObject);
                    };
                },1f);
            } 
        };
    }

    // Update is called once per frame
    void Update()
    {
        state();
    }

    public void set(int rank){
        this.rank = rank;
        GetComponent<TextMeshProUGUI>().text = $"現在 {rank}位！";
    }
}
