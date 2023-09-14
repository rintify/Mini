using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    void Start()
    {
     number=0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(number);
    }
}
