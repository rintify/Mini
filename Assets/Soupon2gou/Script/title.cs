using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    private bool firstPush=false;

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start");
       
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            //ここに次のシーンに行く命令を書く
            SceneManager.LoadScene("game1");
            firstPush = true;
        }
    }
}
