using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Twitter : MonoBehaviour
{
    public Text texual;
    public Text japanese;
    public TextAsset data;
    private string question;
    private int current;
    string[][] lines;
    public AudioClip clip,bu,pinpon;
    bool canPress;
    [NonSerialized]
    public float timeLimit;
    [NonSerialized]
    public TypeMonster monster;
    // Start is called before the first frame update
    void Start()
    {
        lines = data.text.Split(",,").Select(a => a.Split(",")).ToArray();
        next();
        clearAnime = () => {
            this.transform.localScale += 1.17f/timeLimit*Vector3.one*Time.deltaTime;
        };
    }

    void next(){
        canPress = true;
        current = 0;
        texual.text = question;
        var selected = lines[Common.RequiredLevel-1].ElementAtRandom().Split(" ");
        japanese.text = selected[0];
        question = texual.text = selected[1];
    }

    Action clearAnime;

    // Update is called once per frame
    void Update()
    {
        clearAnime();

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
            Common.PlayOneShot(clip,3);
            if(current < question.Length && question[current] == pressed){
                texual.text = $"<color=#00ff00>{question.Substring(0,current+1)}</color>{question.Substring(current+1,question.Length-current-1)}";
                current ++;
                if(current >  question.Length) current = question.Length;
                if(current == question.Length){
                    Common.IsCleared = true;
                    clearAnime = () => {
                        this.transform.localScale *= Mathf.Pow(0.01f, Time.deltaTime*5);
                        if(this.transform.localScale.x < 0.01f){
                            Common.PlayOneShot(pinpon,2.5f);
                            monster.die();
                            clearAnime = () => {};
                            this.Delay(()=>{
                                Common.EndGame();
                            },0.8f);
                        }
                    };
                }
            }
            else{try{
                texual.text = $"<color=#00ff00>{question.Substring(0,current)}</color><color=#ff0033>{pressed}</color>{question.Substring(current+1,question.Length-current-1)}";
                canPress = false;
                Common.PlayOneShot(bu,2.5f);
                this.Delay(()=>{
                    current = 0;
                    canPress = true;
                    texual.text = question;
                },0.2f);
            }catch{}}
        }
        
    }
}
