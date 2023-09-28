using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ステージクリアSE")] public AudioClip stageClearSE;
    [Header("ステージクリア")] public GameObject stageClearObj;
    [Header("ステージクリア判定")] public PlayerClearCheck stageClearTrigger;

    private bool doClear = false;

    // Start is called before the first frame update
    void Start()
    {
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
   
        if(stageClearTrigger != null && stageClearTrigger.isOn2 && !doClear) {
            Debug.Log("ok");
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
    }
}