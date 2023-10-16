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

    //クリア判定
    public bool clear = false;

    // Start is called before the first frame update
    void Start()
    {
        Common.StartGame(8, () => { Common.EndGame(clear); });

        if (playerObj != null && continuePoint != null && continuePoint.Length > 0)
        {
            Debug.Log("プレイヤーは原点に移動しました");
            playerObj.transform.position = continuePoint[0].transform.position;
            //stageClearObj.SetActive(false);
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
            Debug.Log("fallしました");
            playerfall();
            isfall = true;
        }
   
        if(stageClearTrigger != null && stageClearTrigger.isOn2 && !doClear) {
            Debug.Log("clearしました");
            StageClear();
            doClear = true;
        }

       
    }
    /// <summary>
    /// ステージをクリアした
    /// </summary>
    public void StageClear()
    {
        GameManager.instance.isStageClear = true;
        //stageClearObj.SetActive(true);
        GameManager.instance.PlaySE(stageClearSE);
        Debug.Log("ステージクリアSEをならしました");

        //Common.EndGame(true);
    }
    /// <summary>
    /// 落下した
    /// </summary>
    public void playerfall()
    {
        GameManager.instance.PlaySE(fallSE);
        Debug.Log("fallSEをならしました");
    }
}