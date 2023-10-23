using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Twitter : MonoBehaviour
{
    public RectTransform sexy;
    public Text texual;
    public Text japanese;
    public TextAsset data;
    private string question;
    private int current;
    string[][] lines;
    public AudioSource source;
    public AudioClip clip,bu;
    bool canPress;
    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(
            Common.RequiredLevel == 4 ? 15 :
            Common.RequiredLevel == 3 ? 12 :
            10,()=>{
            Common.EndGame(false);
        });
        lines = data.text.Split(",,").Select(a => a.Split(",")).ToArray();
        next();
    }

    void next(){
        canPress = true;
        current = 0;
        texual.text = question;
        var selected = lines[Common.RequiredLevel-1].ElementAtRandom().Split(" ");
        japanese.text = selected[0];
        question = texual.text = selected[1];
    }

    // Update is called once per frame
    void Update()
    {
        if(!canPress) return;
        char pressed = (char)0;
        for(int kcode = (int)KeyCode.A; kcode <= (int)KeyCode.Z; kcode ++)
        {
            if (Input.GetKeyDown((KeyCode)kcode))
            {
                pressed = (char)(kcode - (int)KeyCode.A + 'a');
            }
        }

        if(pressed != 0){
            source.PlayOneShot(clip);
            if(current < question.Length && question[current] == pressed){
                texual.text = $"<color=#00ff00>{question.Substring(0,current+1)}</color>{question.Substring(current+1,question.Length-current-1)}";
                current ++;
                if(current == question.Length)
                this.Delay(()=>{
                    Common.EndGame(true);
                },0.5f);
            }
            else{
                texual.text = $"<color=#00ff00>{question.Substring(0,current)}</color><color=#ff0033>{pressed}</color>{question.Substring(current+1,question.Length-current-1)}";
                canPress = false;
                source.PlayOneShot(bu);
                this.Delay(()=>{
                    current = 0;
                    canPress = true;
                    texual.text = question;
                },0.2f);
            }
        }
        
    }
}
