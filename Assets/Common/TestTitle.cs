using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestTitle : MonoBehaviour
{
    private bool firstPush=false;
    private bool can_do = false;

    InputField inputField;
    string language="Japanese";
    string name="ゲスト";

    private void Start()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        //allow_startを3.5秒後に呼び出す
        Invoke(nameof(allow_start), 4.5f);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            PressStart();
        }
    }

    public void ChangeToJapanese()
    {
        language = "Japanese";
    }

    public void ChangeToEnglish()
    {
        language = "English";
    }

    public void InputUserName() {
        name = inputField.text;
        Debug.Log(name);
        inputField.text = "";
    }

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start");
       
        if (!firstPush && can_do)
        {
            Debug.Log("Go Next Scene!");
            //ここに次のシーンに行く命令を書く
            Common.StartGames(name,language);
            firstPush = true;
        }
    }

    public void allow_start()
    {
        can_do = true;
    }
}
