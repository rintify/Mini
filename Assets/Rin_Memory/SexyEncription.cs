using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SexyEncription : MonoBehaviour
{
    [NonSerialized]
    public Sprite sexyprite;
    private Sprite defaultSp;
    private Discord_Math math;
    private bool virgin;

    private void Awake(){
        defaultSp = this.GetComponent<Image>().sprite;
    }

    private void Start()
    {
    }

    public void set(Sprite sprite,Discord_Math math){
        this.sexyprite = sprite;
        this.GetComponent<Image>().sprite = sexyprite;
        this.math = math;
    }

    public void open(){
        this.GetComponent<Image>().sprite = defaultSp;
        virgin = true;
    }

    public void flip(){
        if(!virgin || !math.listen) return;
        this.GetComponent<Image>().sprite = sexyprite;
        math.notifyFliped(this);
        virgin = false;
    }

    public void OnCardClick()
    {
        flip();
    }
}
