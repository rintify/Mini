using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TestTitle : MonoBehaviour
{
    private bool firstPush=false;
    private bool can_do = false;

    public string username;
    private string language="Japanese";


    InputFieldManager IM; //呼ぶスクリプトにあだなつける
    private void Start()
    {
        
        GameObject obj = GameObject.Find("入力確認"); //オブジェクトを探す
        IM = obj.GetComponent<InputFieldManager>(); //付いているスクリプトを取得
        //allow_startを3.5秒後に呼び出す
        Invoke(nameof(allow_start), 4.5f);
    }

    private void Update() {
        username = IM.name;
        if (Input.GetKeyDown(KeyCode.Space)){
            PressStart();
        }
    }

public void ChangeToJapanese()
    {
        language = "Japanese";
        Debug.Log("ToJap");
    }

    public void ChangeToEnglish()
    {
        language = "English";
        Debug.Log("ToEng");
    }

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start");
       
        if (!firstPush && can_do)
        {
            Debug.Log("Go Next Scene!");
            //ここに次のシーンに行く命令を書く
            Common.StartGames(username,language);
            Debug.Log(username+" "+language);
            firstPush = true;
        }
    }

    public void allow_start()
    {
        can_do = true;
    }
}
