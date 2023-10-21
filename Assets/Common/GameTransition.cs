using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTransition : MonoBehaviour
{
    [SerializeField]
    GameObject img;

    void Start(){

    }

    //次のゲームに遷移する時にコモンから呼ばれる
    public void GameToGame(string nextSceneName){
        void GtoG()
        {
            GameObject Im = transform.Find("Canvas/Image").gameObject;
            Im.GetComponent<Image>().color = new Color(0.8f, 0.3f, 0.1f, 255f);
            var tansitionAnim = img.GetComponent<Animator>();
            tansitionAnim.SetTrigger("out2");
            StartCoroutine(TimeLate());
            SceneManager.LoadScene(nextSceneName);
            Im.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            tansitionAnim.SetTrigger("in2");
            Debug.Log("GtoG");
        }
        IEnumerator TimeLate()
        {
            yield return new WaitForSeconds(1);
        }
        GtoG();
        
    }

    //リザルト画面に遷移する時にコモンから呼ばれる
    public void GameToResult(string resultSceneName){
        IEnumerator Fin()
        {
            GameObject Im = transform.Find("Canvas/Image").gameObject;
            var TansitionAnimResult = img.GetComponent<Animator>();
            TansitionAnimResult.SetTrigger("out");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(resultSceneName);
            TansitionAnimResult.SetTrigger("in");
            Debug.Log("GtoRe");
        }
        StartCoroutine(Fin());
    }
    
}
