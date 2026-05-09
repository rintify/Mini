using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;

public class scoremaneger : MonoBehaviour
{
private TextMeshProUGUI textframe;
    // Start is called before the first frame update
   
    string NewData="ロード中";
    // コルーチンの開始
    //Dictionary<string, string> postData = new Dictionary<string, string>();

    // 送信するデータをキーと値のペアで作成
    private WWWForm formDataadd;
    private WWWForm formDatatop;
    private WWWForm formDatamy;

    void Start()
    {
        textframe = GetComponent<TextMeshProUGUI>();
        //add();
        top();
        //my();
    }
    
    IEnumerator SendPostRequestadd()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(Common.AddResultUrl, formDataadd))
        {
            www.redirectLimit = 10;
            yield return www.SendWebRequest();
            Debug.Log("Response: " + www.downloadHandler.text);

            
            
        }
    }

    void add()
    {
        formDataadd = new WWWForm();
        int score=0;
        string name="none";
        
        //name = Common.PlayerName;
        //score = Common.Score;

        formDataadd.AddField("name", name);
        formDataadd.AddField("score", score);
        formDataadd.AddField("key", "sq9YZY0ZfQA7vI9zK3QIsHawIb");

        // POSTリクエストを送信
        StartCoroutine(SendPostRequestadd());
    }
    IEnumerator SendPostRequesttop()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(Common.GetTopPlayersUrl, formDatatop))
        {
            yield return www.SendWebRequest();

            List<PlayerScore> playerScores = JsonConvert.DeserializeObject<List<PlayerScore>>(www.downloadHandler.text);

            // 名前とスコアを一つのstringに出力
            string output = "";
            foreach (PlayerScore player in playerScores)
            {
                output += $"{player.Name}: {player.Score}\n";
            }

            Debug.Log(output);
            Debug.Log("Response: " + www.downloadHandler.text);
            NewData = output;
        }
    }

    void top()
    {
        formDatatop = new WWWForm();
        

        // POSTリクエストを送信
        StartCoroutine(SendPostRequesttop());
    }


    IEnumerator SendPostRequestmy()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(Common.GetMyResultUrl, formDatamy))
        {
            yield return www.SendWebRequest();


            Debug.Log("Response: " + www.downloadHandler.text);

        }
    }

    void my()
    {
        formDatamy = new WWWForm();
        int score;
        string name;
        score = 39;
        name = "AAA";
        formDatamy.AddField("name", "taro");
        

        // POSTリクエストを送信
        StartCoroutine(SendPostRequestmy());
    }


    // Update is called once per frame
    void Update()
    {
        
        textframe.text=NewData;
    }
}
public class PlayerScore
{
    public string Name { get; set; }
    public int Score { get; set; }
}
