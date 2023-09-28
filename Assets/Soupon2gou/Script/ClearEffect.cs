using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearEffect : MonoBehaviour
{
    //クリアしたら音(SE)を鳴らしてScene移動させる。
    private bool comp = false;
    private float timer;

    private void Update()
    {
        if (!comp)
        {
            if (timer < 8.0f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                //0はタイトルのはず
                SceneManager.LoadScene("titleScene");
                comp = true;
            }
        }
    }
}