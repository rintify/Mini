using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twitter : MonoBehaviour
{
    public Text test;
    public string question;
    private int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        test.text = question;
    }

    // Update is called once per frame
    void Update()
    {
        char pressed = (char)0;
        for(int kcode = (int)KeyCode.A; kcode <= (int)KeyCode.Z; kcode ++)
        {
            if (Input.GetKeyDown((KeyCode)kcode))
            {
                pressed = (char)(kcode - (int)KeyCode.A + 'a');
            }
        }

        if(pressed != 0){
            if(question[current] == pressed){
                Debug.Log("ok");
                current ++;
                test.text = $"<color=#00ff00>{question.Substring(0,current)}</color>{question.Substring(current,question.Length-current)}";
            }
        }
        
    }
}
