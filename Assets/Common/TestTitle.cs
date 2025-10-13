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

    public string username="ゲスト";
    private string language="Japanese";
    public GameObject[] inVisibleObjects;
    public InputFieldManager IM; //呼ぶスクリプトにあだなつける

    private void Start()
    {
        //allow_startを3.5秒後に呼び出す
        Invoke(nameof(allow_start), 4.5f);
        foreach (var o in inVisibleObjects) o.SetActive(false);


        Debug.Log("transparent");
        Invoke(nameof(non_transparent), 4.5f);
    }

    private void Update() {
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
            username = IM.Name;
            Debug.Log("Go Next Scene!");
            //ここに次のシーンに行く命令を書く
            delete(username);
            if (username == "" || username == null)
            {
                username = "ゲスト";
            }
            Common.StartGames(username,language);
            Debug.Log(username+" "+language);
            firstPush = true;
        }
    }


    public static string delete(string str)
    {
        return str.Replace("\r", "").Replace("\n", "").Trim();
    }



    public void allow_start()
    {
        can_do = true;
    }

    public void non_transparent(){
        foreach (var o in inVisibleObjects) o.SetActive(true);


        Debug.Log("nontransparent");
    }
}
