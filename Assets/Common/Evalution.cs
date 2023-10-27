using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evalution : MonoBehaviour
{
    public GameObject awesome;
    public GameObject excellent;
    public GameObject great;
    public GameObject good;
    public GameObject poor;
    // Start is called before the first frame update
    void Start()
    {
       awesome.SetActive(false);
       excellent.SetActive(false);
       great.SetActive(false);
       good.SetActive(false);
       poor.SetActive(false);
        if (Common.Score < 10)
        {
            poor.SetActive(true);
        }
        else if (Common.Score < 20)
        {
            good.SetActive(true);
        }
        else if (Common.Score < 30)
        {
            great.SetActive(true);
        }
        else if (Common.Score < 40)
        {
            excellent.SetActive(true);
        }
        else awesome.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
