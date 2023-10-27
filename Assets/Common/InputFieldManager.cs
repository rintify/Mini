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
    private string postUrl = "https://cas-ru.com/DOdbhfRG9ze37XoF/existName.php";
    private WWWForm formData;
    //出力用のテキスト
    public TMP_InputField Field;
    public new string name =" ";
    public GameObject exist;
    private void Start()
    {
        exist.SetActive(false);
        GetComponent<TMP_Text>().text = "ユーザー名:ゲスト";
    }

    //inputFieldのOnEndEditに設定する用の関数
    public void OnValueChanged()
    {
        string input = Field.GetComponent<TMP_InputField>().text;
        GetComponent<TMP_Text>().text = "ユーザー名:"+input;
        name = input;
        //名前が存在するか確認
        checkname();
    }
    
    void checkname()
    {
        formData = new WWWForm();

        formData.AddField("name", name);
        // POSTリクエストを送信
        StartCoroutine(SendPostRequest());
    }


    IEnumerator SendPostRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(postUrl, formData))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "1")
            {
                exist.SetActive(true);
                Debug.Log("存在する");
            }
            //Debug.Log("Response: " + www.downloadHandler.text);

        }
    }
}
