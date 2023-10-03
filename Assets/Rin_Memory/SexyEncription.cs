using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SexyEncription : MonoBehaviour
{
    public Text cardText;
    private bool omote = false;
    private Sprite sexyprite;
    private Sprite defaultSp;

    private void Awake(){
        defaultSp = this.GetComponent<Image>().sprite;
    }

    private void Start()
    {
        cardText.text = "Initial Text";
    }

    public void set(Sprite sprite){
        this.sexyprite = sprite;
        this.GetComponent<Image>().sprite = sexyprite;
        omote = true;
    }

    public void flip(){
        if (omote)
        {
            cardText.text = "Initial Text";
            omote = false;
            this.GetComponent<Image>().sprite = defaultSp;
        }
        else
        {
            cardText.text = "Clicked Text";
            omote = true;
            this.GetComponent<Image>().sprite = sexyprite;
        }
    }

    public void OnCardClick()
    {
        flip();
    }
}
