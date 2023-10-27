using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Common : MonoBehaviour
{

//*** 各ゲームが実行してほしい ***

    ///<summary>ゲームに要求される難易度</summary>
    public static int RequiredLevel {get{
        return instance.currentScene.scene.level;
    }}

    ///<summary>ゲームを開始する時に実行し、カウントを開始する</summary>
    public static void StartGame(int timeLimit,Action onTimeUp){
        //スタートされてなかったらスタートする
        if(instance.ui == null) StartGame(); 
        //連続呼び出しを避ける
        if(instance.onTimeUp != null) return;
        Debug.Log("Game Start");
        instance.timeLimit = timeLimit;
        instance.onTimeUp = onTimeUp;
        instance.ui.StartTimer();
    }

    ///<summary>カウントを中断し再度開始する</summary>
    public static void RestartTimer(int timeLimit,Action onTimeUp){
        EndTimer();
        instance.timeLimit = timeLimit;
        instance.onTimeUp = onTimeUp;
        instance.ui?.StartTimer();
    }

    ///<summary>ゲームを開始する前に実行し、カウントは開始しない</summary>
    public static void StartGame(){
        //連続呼び出しを避ける
        if(instance.ui == null) StartUI();
        Debug.Log("Game Awake");
    }

    ///<summary>ゲームを終了する前に実行し、カウントを終了する onTimeupは実行されない</summary>
    public static void EndTimer(){
        //連続呼び出しを避ける
        if(instance.onTimeUp == null) return;
        Debug.Log("End Timer");
        instance.ui.BreakTimer();
        instance.onTimeUp = null;
    }

    ///<summary>ゲームノクリア判定を設定</summary>
    public static bool IsCleared {set {
        if(instance.isCleared != 0) return;
        instance.isCleared = value ? 1 : -1;
    } get{return instance.isCleared == 1;}}

    ///<summary>ゲームノクリア判定を設定</summary>
    public static bool IsOvered {get{return instance.isCleared == -1;}}

    ///<summary>ゲームを終了する</summary>
    public static void EndGame(){
        //シーンが切り替わるまでの間連続呼び出しを避ける
        if(instance.ui == null) return;
        Debug.Log(instance.isCleared == 1 ? "Game Clear" : "Game Over");
        //リセット
        Destroy(instance.ui.gameObject);
        instance.onTimeUp = null;
        instance.timeLimit = 10;
        instance.ui = null;
        instance.Delay(() => {
            instance.audioSource.Stop();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        },0.7f);
        

        //クリアしたらスコア+1
        if(instance.isCleared == 1){
            instance.score ++;
            int preLevel = instance.level;
            //スコアに応じてレベルを決める
            instance.level = 
                instance.score >= 30 ? 4 :
                instance.score >= 20 ? 3 :
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

    ///<summary>ゲームを終了し次のゲームを開始する</summary>
    ///<param name="isCleared">ゲームをクリアできたか</param>
    public static void EndGame(bool isCleared){
        IsCleared = isCleared;
        EndGame();
    }






//*** スタート画面が実行してほしい ***

    ///<summary>スタート画面からゲームを開始する</summary>
    public static void StartGames(string playerName,string langage){
        instance.playerName = playerName;
        instance.langage = langage;
        instance.Next();
    }




//*** 共通の情報 ***

    ///<summary>プレイヤーの名前</summary>
    public static string PlayerName {get{
        return instance.playerName;
    }}

    ///<summary>ライフ</summary>
    public static int Life {get{
        return instance.life;
    }}

    ///<summary>スコア</summary>
    public static int Score {get{
        return instance.score;
    }}

    ///<summary>現在のレベル</summary>
    public static int Level {get{
        return instance.level;
    }}




//*** 各ゲームの情報 ***

    ///<summary>タイトル</summary>
    public static string Title {get{
        return instance.langage == "Japanese" ?
            instance.currentScene.game.title : 
            instance.currentScene.game.titleEng;
    }}

    ///<summary>操作方法</summary>
    public static string Instruction {get{
        return instance.langage == "Japanese" ? 
            instance.currentScene.game.instruction : 
            instance.currentScene.game.instructionEng;
    }}



//*** タイマーが実行してほしい ***

    ///<summary>制限時間</summary>
    public static int TimeLimit {get{
        return instance.timeLimit;
    }}

    ///<summary>制限時間が切れたら実行</summary>
    public static void TimeUp(){
        instance.onTimeUp?.Invoke();
    }

    ///<summary>次のゲームシーンに遷移したら実行</summary>
    public static void StartUI(){
        instance.ui = Instantiate(instance.uiPrefab, instance.canvas.transform);
        instance.ui.transform.SetAsLastSibling();
    }

    public static void ToTitle(){
        Destroy(instance.gameObject);
        SceneManager.LoadScene(instance.titleScene);
    }



//*** その他便利機能 ***

    public static void PlayOneShot(AudioClip clip){
        instance.audioSource.PlayOneShot(clip);
    }

    public static void PlayOneShot(AudioClip clip, float vol){
        instance.audioSource.PlayOneShot(clip,vol);
    }








    static Common instance;
    [SerializeField]
    TextAsset gamesData;
    Game[] games;

    [SerializeField]
    Canvas canvas;

    UIManager ui;
    [SerializeField]
    UIManager uiPrefab;

    [SerializeField]
    string resultScene;
    [SerializeField]
    string titleScene;

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
    readonly List<(Scene scene,Game game)> scenesAtLevel = new();

    //今のゲーム
    (Scene scene,Game game) currentScene;
    int timeLimit;
    Action onTimeUp;
    int isCleared;


    void Awake() {
        //唯一無二
        if(instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        //不滅
        DontDestroyOnLoad(gameObject);

        //各ゲームのデータをJSONから取得
        games = JsonConvert.DeserializeObject<Game[]>(gamesData.text);
        //テストプレイ用
        currentScene = (new Scene(){
            level = level,
            name = ""
        },new Game(){
            instruction = "X: dot",
            instructionEng = "X: dot",
            title = "Do!",
            titleEng = "Do!"
        });

        //初期レベルのゲームリストを作成
        Reselect();

        transition = GetComponent<GameTransition>();
        audioSource = GetComponent<AudioSource>();
    }

    //今の難易度に対応したゲームリストを作成
    void Reselect(){
        scenesAtLevel.Clear();
        SelectGamesLevel(level,-1);
        int c = scenesAtLevel.Count;
        if(level == 1){
        }
        else if(level == 2){
            SelectGamesLevel(1,Mathf.CeilToInt(0.7f*c));
        }
        else if(level == 3){
            SelectGamesLevel(2,Mathf.CeilToInt(0.5f*c));
            SelectGamesLevel(1,Mathf.CeilToInt(0.2f*c));
        }
        else{
            SelectGamesLevel(3,Mathf.CeilToInt(0.4f*c));
            SelectGamesLevel(2,Mathf.CeilToInt(0.2f*c));
            SelectGamesLevel(1,Mathf.CeilToInt(0.1f*c));
        }

        Debug.Log($"select total -> {scenesAtLevel.Count} level:{level} -> {c}");
    }

    void SelectGamesLevel(int level, int max){
        var a = games.Select(game => {
            //要求される難易度かつ今のシーンでないシーンを選択 -> シーンの連続を避ける
            var s = game.scenes.Where(s => 
                s.level == level && s != currentScene.scene
            );
            //それがないゲームを除く
            if(s.Count() == 0) return (null,null);

            return (s.ElementAtRandom(),game);
        }).Where(g => g.Item1 != null).ToList();

        if(max != -1){
            a.Shuffle();
            if(a.Count > max) a.RemoveRange(max,a.Count - max);
        }
        
        scenesAtLevel.AddRange(a);
    }

    //ライフに応じて次のゲームorリザルト画面
    void Next(){
        //ライフが0になったらFinでリザルト画面へ遷移
        if(instance.life <= 0){
            ToResult();
        }
        //次のゲームへ遷移
        else{
            //ゲームリストが空なら補充
            if(scenesAtLevel.Count == 0) Reselect();
            if(scenesAtLevel.Count == 0){
                Debug.Log("No Game");
                //SceneManager.LoadScene(resultScene);
            }
            //ランダムにゲームを選んで抜く
            currentScene = scenesAtLevel.ElementAtRandom();
            scenesAtLevel.Remove(currentScene);

            isCleared = 0;
            
            //StartCoroutine(LoadSceneInBackground(currentScene.scene.name));
            
            StartCoroutine(WaitTrans());
        }

        IEnumerator WaitTrans()
        {
            transition.GameToTrans();
            yield return new WaitForSeconds(3);
            transition.TransToGame(currentScene.scene.name);
        }
    }

    async void ToResult(){
        Debug.Log("add REsult");
        await addresult();
        Debug.Log("added REsult");
        //名前とスコアをサーバーに転送
        transition.GameToResult(resultScene);
    }


    //データベース
    private string postUrladd = "https://cas-ru.com/DOdbhfRG9ze37XoF/addResult.php";
    private WWWForm formDataadd;
    IEnumerator SendPostRequestadd(TaskCompletionSource<bool> tcs)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(postUrladd, formDataadd))
        {
            www.redirectLimit = 10;
            yield return www.SendWebRequest();
            Debug.Log("Response: " + www.downloadHandler.text);
            tcs.SetResult(www.downloadHandler.text == "Result");
        }
    }

    Task addresult()
    {
        var tcs = new TaskCompletionSource<bool>();
        formDataadd = new WWWForm();
        int score = Common.Score;
        string name;
        if (PlayerName == ""|| PlayerName==null)
        {
            name = "Guest";
        }else name = PlayerName;

        //name = Common.PlayerName;
        //score = Common.Score;

        formDataadd.AddField("name", name);
        formDataadd.AddField("score", score);
        formDataadd.AddField("key", "sq9YZY0ZfQA7vI9zK3QIsHawIb");

        // POSTリクエストを送信
        StartCoroutine(SendPostRequestadd(tcs));

        return tcs.Task;
    }
    /*
        private AsyncOperation asyncLoad;

        private IEnumerator LoadSceneInBackground(string sceneName)
        {
            asyncLoad = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            // シーンが読み込まれるのを待つ
            while (!asyncLoad.isDone)
            {
                // asyncLoad.progressはallowSceneActivationがfalseの場合、最大0.9までしか進まない
                if (asyncLoad.progress >= 0.9f)
                {
                    // ここで何らかの処理を行い、シーンをスタートするタイミングを待つ
                    // 例: ボタンが押されたら、シーンをスタートするなど

                    // 以下はデモのためのコードで、3秒待った後にシーンをスタートします
                    yield return new WaitForSeconds(1f);
                    asyncLoad.allowSceneActivation = true;


                    var loadedScene = SceneManager.GetSceneByName(sceneName);
                    if (loadedScene.isLoaded)
                    {
                        foreach (GameObject obj in loadedScene.GetRootGameObjects())
                        {
                            Camera cam = obj.GetComponentInChildren<Camera>();
                            if (cam)
                            {
                                cam.depth = -1;
                            }
                        }
                    }
                }

                yield return null;
            }
        }*/


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