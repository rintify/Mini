using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    public static int number=0;
    // Start is called before the first frame update
    void Start()
    {
    //Common.StartGame(8,Common.EndGame(false));
     number=0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(number);
        if (number == movement.enemy)
        {
            Common.EndGame(true);

        }
        /*if(Common.TimeLimit==0)
        {
            Common.TimeUp();
        }*/
    }
}
