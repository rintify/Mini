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


    InputFieldManager IM; //呼ぶスクリプトにあだなつける
    private void Start()
    {
        
        //allow_startを3.5秒後に呼び出す
        Invoke(nameof(allow_start), 4.5f);

        GameObject obj1 = GameObject.Find("input_name"); //オブジェクトを探す
        GameObject obj2 = GameObject.Find("InputField"); //オブジェクトを探す
        GameObject obj3 = GameObject.Find("入力確認"); //オブジェクトを探す
        GameObject obj4 = GameObject.Find("日本語"); //オブジェクトを探す
        GameObject obj5 = GameObject.Find("言語選択"); //オブジェクトを探す
        GameObject obj6 = GameObject.Find("英語"); //オブジェクトを探す
        GameObject obj7 = GameObject.Find("InputField/Text Area/na"); //オブジェクトを探す
        GameObject obj8 = GameObject.Find("日本語/jp"); //オブジェクトを探す
        GameObject obj9 = GameObject.Find("英語/en"); //オブジェクトを探す
        GameObject obj10 = GameObject.Find("ランキング"); //オブジェクトを探す
        GameObject obj11 = GameObject.Find("ランキング/ran"); //オブジェクトを探す

        obj1.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj2.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj3.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj4.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj5.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj6.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj7.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj8.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj9.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj10.GetComponent<CanvasRenderer>().SetAlpha(0);
        obj11.GetComponent<CanvasRenderer>().SetAlpha(0);

        Debug.Log("transparent");
        IM = obj3.GetComponent<InputFieldManager>(); //付いているスクリプトを取得
        Invoke(nameof(non_transparent), 4.5f);
    }

    private void Update() {
        
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
            username = IM.name;
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

    public void non_transparent(){
        GameObject obj1 = GameObject.Find("input_name"); //オブジェクトを探す
        GameObject obj2 = GameObject.Find("InputField"); //オブジェクトを探す
        GameObject obj3 = GameObject.Find("入力確認"); //オブジェクトを探す
        GameObject obj4 = GameObject.Find("日本語"); //オブジェクトを探す
        GameObject obj5 = GameObject.Find("言語選択"); //オブジェクトを探す
        GameObject obj6 = GameObject.Find("英語"); //オブジェクトを探す
        GameObject obj7 = GameObject.Find("InputField/Text Area/na"); //オブジェクトを探す
        GameObject obj8 = GameObject.Find("日本語/jp"); //オブジェクトを探す
        GameObject obj9 = GameObject.Find("英語/en"); //オブジェクトを探す
        GameObject obj10 = GameObject.Find("ランキング"); //オブジェクトを探す
        GameObject obj11 = GameObject.Find("ランキング/ran"); //オブジェクトを探す

        obj1.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj2.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj3.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj4.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj5.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj6.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj7.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj8.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj9.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj10.GetComponent<CanvasRenderer>().SetAlpha(1);
        obj11.GetComponent<CanvasRenderer>().SetAlpha(1);


        Debug.Log("nontransparent");
    }
}
