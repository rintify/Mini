using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twitter : MonoBehaviour
{
    public Text texual;
    public Text japanese;
    public TextAsset data;
    private string question;
    private int current;
    string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        lines = data.text.Split("\n");
        next();
    }

    void next(){
        current = 0;
        texual.text = question;
        var selected = lines[Random.Range(0,lines.Length)].Split(" ");
        japanese.text = selected[0];
        question = texual.text = selected[1];
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
            if(current < question.Length && question[current] == pressed){
                texual.text = $"<color=#00ff00>{question.Substring(0,current+1)}</color>{question.Substring(current+1,question.Length-current-1)}";
                current ++;
                if(current == question.Length)
                this.Delay(()=>{
                    next();
                },0.5f);
            }
            else{
                texual.text = $"<color=#00ff00>{question.Substring(0,current)}</color><color=#ff0033>{pressed}</color>{question.Substring(current+1,question.Length-current-1)}";
            }
        }
        
    }
}
