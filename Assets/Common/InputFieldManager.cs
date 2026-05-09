using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class InputFieldManager : MonoBehaviour
{
    private WWWForm formData;
    //出力用のテキスト
    public TMP_InputField Field;
    string _name = "";
    public string Name {
        get {
            return _name == "" ? "ゲスト" : _name;
        }
        set{
            Field.text = value;
        }
    }
    public GameObject exist;
    private void Start()
    {
        exist.SetActive(false);
        //前のプレイのプレイヤー名を引き継ぐ
        if(Common.SuperCommonData.PlayerName != null){
            Name = Common.SuperCommonData.PlayerName;
        }
        GetComponent<TMP_Text>().text = "ユーザー名:" + Name;
        //120秒後に自動で名前をリセット
        this.Delay(() => {
            Name = "";
        },120f);
    }

    //inputFieldのOnEndEditに設定する用の関数
    public void OnValueChanged()
    {
        _name = Field.text;
        GetComponent<TMP_Text>().text = "ユーザー名:" + Name;
        //名前が存在するか確認
        checkname();
    }
    
    void checkname()
    {
        formData = new WWWForm();

        formData.AddField("name", Name);
        // POSTリクエストを送信
        StartCoroutine(SendPostRequest());
    }


    IEnumerator SendPostRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(Common.ExistNameUrl, formData))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "1")
            {
                exist.SetActive(true);
                Debug.Log("存在する");
            }else exist.SetActive(false);
            //Debug.Log("Response: " + www.downloadHandler.text);

        }
    }
}
