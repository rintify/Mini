using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Common : MonoBehaviour
{
    static Common instance;
    [SerializeField]
    TextAsset gamesData;
    Game[] games;
    List<(Scene scene,Game game)> scenesAtLevel;
    (Scene scene,Game game) currentScene;
    [SerializeField]
    int life = 3;
    int score = 0;
    int level = 1;
    string langage;
    string playerName;

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
    }

    ///<summary>ライフ</summary>
    public static int Life {get{return instance.life;}}

    ///<summary>スコア</summary>
    public static int Score {get{return instance.score;}}

    ///<summary>タイトル</summary>
    public static string Title {get{
        return instance.langage == "Japanese" ?
            instance.currentScene.game.title : 
            instance.currentScene.game.titleEng;
    }}

    ///<summary>プレイヤーの名前</summary>
    public static string PlayerName {get{
        return instance.playerName;
    }}

    ///<summary>タイムリミット</summary>
    public static float TimeLimit {get{
        return instance.currentScene.scene.timeLimit;
    }}

    ///<summary>操作方法</summary>
    public static string Instruction {get{
        return instance.langage == "Japanese" ? 
            instance.currentScene.game.instruction : 
            instance.currentScene.game.instructionEng;
    }}

    ///<summary>ゲームに要求される難易度</summary>
    public static int RequiredLevel {get{
        return instance.level;
    }}

    ///<summary>タイトル画面からゲームを開始する</summary>
    public static void StartGames(string playerName,string langage){
        instance.playerName = playerName;
        instance.langage = langage;
        instance.Next();
    }

    public static void StartGames(){
        StartGames("aaa","japanese");
    }

    ///<summary>ゲームを終了し次のゲームを開始する</summary>
    ///<param name="isCleared">ゲームをクリアできたか</param>
    public static void EndGame(bool isCleared){
        //クリアしたらスコア+1
        if(isCleared){
            instance.score ++;
            //スコアに応じてレベルアップ
            if(
                instance.score == 5 ||
                instance.score == 10 ||
                instance.score == 15 ||
                instance.score == 20
            ){
                instance.level ++;
                //レベルに応じてゲームリストを選びなおす
                instance.SelectGamesAtLevel();
            }
        }
        //クリアしなかったらライフを-1
        else instance.life--;
        //次のゲームorリザルト画面へ遷移
        instance.Next();
    }

    //今の難易度にあったゲームリストを作成
    void SelectGamesAtLevel(){
        scenesAtLevel = games.Select(game => {
            var s = game.scenes.Where(s =>
                s.level <= level && level <= s.level
            );
            if(s.Count() == 0) return (null,game);
            return (s.ElementAtRandom(),game);
        }).Where(g => g.Item1 != null).ToList();
    }

    //ライフに応じて次のゲームorリザルト画面
    void Next(){
        //ライフが0になったらリザルト画面へ遷移
        if(instance.life <= 0){
            SceneManager.LoadScene("testResult");
        }
        //次のゲームへ遷移
        else{
            //ゲームリストが空なら補充
            if(scenesAtLevel.Count == 0) SelectGamesAtLevel();
            if(scenesAtLevel.Count == 0){
                SceneManager.LoadScene("testResult");
                return;
            }
            //ランダムにゲームを選んで抜く
            currentScene = scenesAtLevel.ElementAtRandom();
            scenesAtLevel.Remove(currentScene);
            
            SceneManager.LoadScene(
                currentScene.scene.name
            );
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
    public float timeLimit; //制限時間
}