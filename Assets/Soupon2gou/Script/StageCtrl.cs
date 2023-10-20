using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ステージクリアSE")] public AudioClip stageClearSE;
    [Header("落下SE")] public AudioClip fallSE;
    [Header("ステージクリア")] public GameObject stageClearObj;
    [Header("ステージクリア判定")] public PlayerClearCheck stageClearTrigger;
    [Header("落下判定")] public PlayerfallCheck PlayerfallTrigger;

    private bool doClear = false;
    private bool isfall = false;

    //sound Get
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () => { Common.EndGame(false); });
        audioSource = GetComponent<AudioSource>();
        if (playerObj != null && continuePoint != null && continuePoint.Length > 0)
        {
            Debug.Log("プレイヤーは原点に移動しました");
            playerObj.transform.position = continuePoint[0].transform.position;
        }
        else
        {
            Debug.Log("設定が足りてないよ！");
        }
    }

    void Update()
    {
        if(PlayerfallTrigger != null && PlayerfallTrigger.isOn && !isfall)
        {
            isfall = true;
            Debug.Log("fallしました");
            StartCoroutine(Fin_F());
        }
   
        if(stageClearTrigger != null && stageClearTrigger.isOn2 && !doClear) {
            doClear = true;
            Debug.Log("clearしました");
            StartCoroutine(Fin_C());
        }

       
    }

     IEnumerator Fin_C()
    {
        //音楽を鳴らす
        audioSource.PlayOneShot(stageClearSE);
        Debug.Log("ステージクリアSEをならしました");

        //終了まで待機
        yield return new WaitWhile(() => audioSource.isPlaying);

        Common.EndGame(true);
    }

    IEnumerator Fin_F()
    {
        //音楽を鳴らす
        audioSource.PlayOneShot(fallSE);
        Debug.Log("fallSEをならしました");

        //終了まで待機
        yield return new WaitWhile(() => audioSource.isPlaying);

        Common.EndGame(false);
    }
}