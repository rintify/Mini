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
    public void GameToTrans()
    {
        IEnumerator GtoG()
        {
            GameObject Im = transform.Find("Canvas/Image").gameObject;
            Im.GetComponent<Image>().color = new Color(0.1f, 1.0f, 0.3f, 1.0f);
            var transitionAnim = Im.GetComponent<Animator>();
            transitionAnim.SetTrigger("out2");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene($"TransScene{Common.Level}");
            transitionAnim.SetTrigger("in2");
            Debug.Log("GtoG");
        }


        StartCoroutine(GtoG());

    }
    public void TransToGame(string nextSceneName){
        IEnumerator GtoG()
        {
            this.Delay(Common.StartUI,0.6f);
            GameObject Im = transform.Find("Canvas/Image").gameObject;
            Im.GetComponent<Image>().color = new Color(0.1f, 1.0f, 0.3f, 1.0f);
            var transitionAnim = Im.GetComponent<Animator>();
            transitionAnim.SetTrigger("out2");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(nextSceneName);
            transitionAnim.SetTrigger("in2");
            Debug.Log("GtoG");
        }


        StartCoroutine(GtoG());

    }

    //リザルト画面に遷移する時にコモンから呼ばれる

    public Color color;
    public void GameToResult(string resultSceneName){
        IEnumerator Fin()
        {
            GameObject Im = transform.Find("Canvas/Image").gameObject;
            //Im.GetComponent<Image>().color = color;
            Im.GetComponent<Image>().color = new Color(0.1f, 1.0f, 0.3f, 1.0f);
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
