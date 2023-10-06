using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins : MonoBehaviour
{
    public Text Coin;
    public int many = 0;
    public bool clear = false;
    public int max;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Coin.text = ":" +  many.ToString("0") + "/" + max.ToString("0"); 
        if(many == max)
        {
            clear = true;
        }
    }
}
