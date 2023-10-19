using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
        instance.ui = Instantiate(instance.uiPrefab, instance.canvas.transform);
        instance.ui.transform.SetAsLastSibling();
    }

    ///<summary>ゲームを終了し次のゲームを開始する</summary>
    ///<param name="isCleared">ゲームをクリアできたか</param>
    public static void EndGame(bool isCleared){
        Debug.Log(isCleared ? "GameClear" : "GameOver");

        if(!instance){
            return;
        }
        //シーンが切り替わるまでの間連続呼び出しを避ける
        if(instance.onTimeUp == null) return;
        //リセット
        instance.onTimeUp = null;
        instance.timeLimit = 10;
        Destroy(instance.ui);

        //クリアしたらスコア+1
        if(isCleared){
            instance.score ++;
            int preLevel = instance.level;
            //スコアに応じてレベルを決める
            instance.level = 
                instance.score >= 20 ? 4 :
                instance.score >= 15 ? 3 :
                instance.score >= 10 ? 2 :
                1;

            if(instance.level != preLevel){
                //レベルが変わっていればゲームリストを選びなおす
                instance.Reselect();
            }
        }
        //クリアしなかったらライフを-1
        else instance.life--;

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



//*** その他便利機能 ***

    public static void PlayOneShot(AudioClip clip){
        instance.audioSource.PlayOneShot(clip);
    }








    static Common instance;
    [SerializeField]
    TextAsset gamesData;
    Game[] games;

    [SerializeField]
    Canvas canvas;

    GameObject ui;
    [SerializeField]
    GameObject uiPrefab;

    [SerializeField]
    string resultScene;

    GameTransition transition;
    AudioSource audioSource;


    //共通パラメータ
    [SerializeField]
    int life = 3;
    [SerializeField]
    int score = 0;
    string langage;
    string playerName;

    //今のレベル
    [SerializeField]
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
        Reselect();

        transition = GetComponent<GameTransition>();
        audioSource = GetComponent<AudioSource>();
    }

    //今の難易度に対応したゲームリストを作成
    void Reselect(){
        scenesAtLevel = SelectGamesLevel(level,-1);
        int c = scenesAtLevel.Count;
        if(level == 1){
        }
        else if(level == 2){
            scenesAtLevel.AddRange(
                SelectGamesLevel(1,Mathf.CeilToInt(0.7f*c))
            );
        }else if(level == 3){
            scenesAtLevel.AddRange(
                SelectGamesLevel(2,Mathf.CeilToInt(0.5f*c))
            );
            scenesAtLevel.AddRange(
                SelectGamesLevel(1,Mathf.CeilToInt(0.2f*c))
            );
        }
        else{
            scenesAtLevel.AddRange(
                SelectGamesLevel(3,Mathf.CeilToInt(0.4f*c))
            );
            scenesAtLevel.AddRange(
                SelectGamesLevel(2,Mathf.CeilToInt(0.2f*c))
            );
            scenesAtLevel.AddRange(
                SelectGamesLevel(1,Mathf.CeilToInt(0.1f*c))
            );
        }

        Debug.Log("select" + scenesAtLevel.Count + "/" + c);
    }

    List<(Scene,Game)> SelectGamesLevel(int level, int max){
        var a = games.Select(game => {
            var s = game.scenes.Where(s => s.level == level);
            if(s.Count() == 0) return (null,game);
            return (s.ElementAtRandom(),game);
        }).Where(g => g.Item1 != null && g.game != currentScene.game).ToList();
        if(max != -1){
            a.Shuffle();
            if(a.Count > max) a.RemoveRange(max,a.Count - max);
        }
        return a;
    }

    //ライフに応じて次のゲームorリザルト画面
    void Next(){
        //ライフが0になったらFinでリザルト画面へ遷移
        if(instance.life <= 0){
            transition.GameToResult(resultScene);
        }
        //次のゲームへ遷移
        else{
            //ゲームリストが空なら補充
            if(scenesAtLevel.Count == 0) Reselect();
            if(scenesAtLevel.Count == 0){
                Debug.Log("No Game");
                //SceneManager.LoadScene(resultScene);
                return;
            }
            //ランダムにゲームを選んで抜く
            currentScene = scenesAtLevel.ElementAtRandom();
            scenesAtLevel.Remove(currentScene);
            
            transition.GameToGame(currentScene.scene.name);
        } 
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