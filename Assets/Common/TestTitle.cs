using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTitle : MonoBehaviour
{
    private bool firstPush=false;
    private bool can_do = false;
    MathEX.Virgin starterSet;

    private void Start()
    {
        //allow_startを3.5秒後に呼び出す
        Invoke(nameof(allow_start), 4.5f);
        starterSet = new(()=>{
            Debug.Log("Press Start");
       
            if (!firstPush && can_do)
            {
                Debug.Log("Go Next Scene!");
                //ここに次のシーンに行く命令を書く
                Common.StartGames("Alice","Japanese");
                firstPush = true;
            }
        });
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            starterSet.Break();
        }
    }

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        starterSet.Break();
    }

    public void allow_start()
    {
        can_do = true;
    }
}
