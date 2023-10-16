using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Common : MonoBehaviour
{

//*** 各ゲームが実行してほしい ***

    ///<summary>ゲームに要求される難易度</summary>
    public static int RequiredLevel {get{
        if(!instance) return 1;
        return instance.level;
    }}

    ///<summary>ゲームを開始する時に実行する</summary>
    public static void StartGame(int timeLimit,Action onTimeUp){
        if(!instance) return;
        instance.timeLimit = timeLimit;
        instance.onTimeUp = onTimeUp;
        instance.OnStartGame();
    }

    ///<summary>ゲームを終了し次のゲームを開始する</summary>
    ///<param name="isCleared">ゲームをクリアできたか</param>
    public static void EndGame(bool isCleared){
        Debug.Log(isCleared ? "GameClear" : "GameOver");
        if(!instance){
            return;
        }
        //クリアしたらスコア+1
        if(isCleared){
            instance.score ++;
            //スコアに応じてレベルアップ
            if(
                instance.score == 10 ||
                instance.score == 15 ||
                instance.score == 20 ||
                instance.score == 25
            ){
                instance.level ++;
                //レベルに応じてゲームリストを選びなおす
                instance.SelectGamesAtLevel();
            }
        }
        //クリアしなかったらライフを-1
        else instance.life--;

        //リセット
        instance.onTimeUp = null;
        instance.timeLimit = 10;
        Destroy(instance.timer);

        //次のゲームorリザルト画面へ遷移
        instance.Next();
    }






//*** スタート画面が実行してほしい ***

    ///<summary>スタート画面からゲームを開始する</summary>
    public static void StartGames(string playerName,string langage){
        if(!instance){
            Debug.Log("Start!");
            return;
        }
        instance.playerName = playerName;
        instance.langage = langage;
        instance.Next();
    }




//*** 共通の情報 ***

    ///<summary>プレイヤーの名前</summary>
    public static string PlayerName {get{
        if(!instance) return "Alice";
        return instance.playerName;
    }}

    ///<summary>ライフ</summary>
    public static int Life {get{
        if(!instance) return 1;
        return instance.life;
    }}

    ///<summary>スコア</summary>
    public static int Score {get{
        if(!instance) return 1;
        return instance.score;
    }}




//*** 各ゲームの情報 ***

    ///<summary>タイトル</summary>
    public static string Title {get{
        if(!instance) return "Test!";
        return instance.langage == "Japanese" ?
            instance.currentScene.game.title : 
            instance.currentScene.game.titleEng;
    }}

    ///<summary>操作方法</summary>
    public static string Instruction {get{
        if(!instance) return "A: Left B: Right";
        return instance.langage == "Japanese" ? 
            instance.currentScene.game.instruction : 
            instance.currentScene.game.instructionEng;
    }}



//*** タイマーが実行してほしい ***

    ///<summary>制限時間</summary>
    public static int TimeLimit {get{
        if(!instance) return 10;
        return instance.timeLimit;
    }}

    ///<summary>制限時間が切れたら実行</summary>
    public static void TimeUp(){
        if(!instance){
            Debug.Log("TimeUp!");
            return;
        }
        instance.onTimeUp?.Invoke();
    }











    static Common instance;
    [SerializeField]
    TextAsset gamesData;
    Game[] games;

    [SerializeField]
    Canvas canvas;

    GameObject timer;
    [SerializeField]
    GameObject timerPrefab;
    [SerializeField]
    Vector2 timerPosition = new(0.5f,0.5f);
    [SerializeField]
    float timerSize = 0.1f;

    [SerializeField]
    GameObject img;

    [SerializeField]
    string resultScene;


    //共通パラメータ
    [SerializeField]
    int life = 3;
    int score = 0;
    string langage;
    string playerName;

    //今のレベル
    int level = 1;
    List<(Scene scene,Game game)> scenesAtLevel;

    //今のゲーム
    (Scene scene,Game game) currentScene;
    int timeLimit;
    Action onTimeUp;


    void Awake() {
        //唯一無二
        if(instance == null) instance = this;
        else Destroy(gameObject);

        //不滅
        DontDestroyOnLoad(gameObject);

        //各ゲームのデータをJSONから取得
        games = JsonConvert.DeserializeObject<Game[]>(gamesData.text);
        currentScene = (games[0].scenes.ElementAtRandom(),games[0]);

        //初期レベルのゲームリストを作成
        SelectGamesAtLevel();

        img.GetComponent<Image>().color = Color.clear;
    }


    //今の難易度に対応したゲームリストを作成
    void SelectGamesAtLevel(){
        scenesAtLevel = games.Select(game => {
            var s = game.scenes.Where(s => s.level == level);
            if(s.Count() == 0) return (null,game);
            return (s.ElementAtRandom(),game);
        }).Where(g => g.Item1 != null).ToList();
    }

    //ライフに応じて次のゲームorリザルト画面
    void Next(){
        //ライフが0になったらFinでリザルト画面へ遷移
        if(instance.life <= 0){
            StartCoroutine(Fin());
        }
        //次のゲームへ遷移
        else{
            //ゲームリストが空なら補充
            if(scenesAtLevel.Count == 0) SelectGamesAtLevel();
            if(scenesAtLevel.Count == 0){
                Debug.Log("No Game");
                SceneManager.LoadScene(resultScene);
                return;
            }
            //ランダムにゲームを選んで抜く
            currentScene = scenesAtLevel.ElementAtRandom();
            scenesAtLevel.Remove(currentScene);
            
            SceneManager.LoadScene(currentScene.scene.name);
        } 
    }

    IEnumerator Fin()
    {
        Debug.Log("aaa");
        var tansitionAnim1 = img.GetComponent<Animator>();
        tansitionAnim1.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(resultScene);
        tansitionAnim1.SetTrigger("Start");
    }

    void OnStartGame(){
        //タイマーの場所大きさを決定
        timer = Instantiate(timerPrefab, canvas.transform);
        timer.transform.SetAsLastSibling();
        var rectTransform = timer.GetComponent<RectTransform>();
        rectTransform.anchorMin = timerPosition;
        rectTransform.anchorMax = rectTransform.anchorMin;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        float referenceSize = canvas.pixelRect.width;
        float targetSize = referenceSize * timerSize;
        float scaleFactor = targetSize / rectTransform.rect.width;
        rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}

class Game{
    public string title; //タイトル
    public string titleEng; //英語のタイトル
    public string instruction; //操作方法
    public string instructionEng; //英語の操作方法
    public Scene[] scenes; //同じタイトルのシーン
}

class Scene{
    public string name; //シーン名
    public int level; //難易度 1~4
}