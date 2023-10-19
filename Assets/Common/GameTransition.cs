using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTransition : MonoBehaviour
{
    [SerializeField]
    GameObject img;

    void Start(){

    }

    //次のゲームに遷移する時にコモンから呼ばれる
    public void GameToGame(string nextSceneName){
        SceneManager.LoadScene(nextSceneName);
    }

    //リザルト画面に遷移する時にコモンから呼ばれる
    public void GameToResult(string resultSceneName){
        IEnumerator Fin()
        {
            var tansitionAnim1 = img.GetComponent<Animator>();
            tansitionAnim1.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(resultSceneName);
            tansitionAnim1.SetTrigger("Start");
        }
        StartCoroutine(Fin());
    }
    
}
