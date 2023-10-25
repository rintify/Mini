using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class scoremaneger : MonoBehaviour
{
private TextMeshProUGUI textframe;
    // Start is called before the first frame update
    static string URL_SELECT = "";
    string NewData;
    // コルーチンの開始
    IEnumerator Start()
    {
        textframe = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Write());
        // UnityWebRequestを作成してURL_SELECTのページにアクセス
        UnityWebRequest request = UnityWebRequest.Get(URL_SELECT);

        yield return request.SendWebRequest();  // リクエストを送信し、レスポンスを待つ

        if (request.result == UnityWebRequest.Result.Success) // レスポンスの結果をチェック
        {
            // レスポンスデータを取得
            string data = request.downloadHandler.text;

            // HTMLエンコードされた文字列をデコード
            string decodedData = System.Web.HttpUtility.HtmlDecode(data);

            // 改行タグを実際の改行文字に変換
            NewData = decodedData.Replace("<br>", "\n");
            //Debug.Log(NewData);
        }
        else
        {
            Debug.LogError("WebAPI Error: " + request.error);
        }
    }
    private IEnumerator Write()
    {
        string url = URL_SELECT;

        //WWWForm:WWWクラスを使用してwebサーバにポストするフォームデータを生成するヘルパークラス
        WWWForm wwwForm = new WWWForm();

        //AddFieldでfieldに値を格納                
        wwwForm.AddField("player_name", Common.PlayerName);
        wwwForm.AddField("score", Common.Score);

        //WWWオブジェクトにURL,WWWFormをセットすることでPOST,GETを行える。
        WWW www = new WWW(url, wwwForm);

        //実行
        yield return www;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        textframe.text=NewData;
    }
}
