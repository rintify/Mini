using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("スコア")] public int score;
    [HideInInspector] public bool isStageClear=false;

    private AudioSource audioSource = null;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("thisをインスタンスに入れる");
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("this.gameObjectの削除");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// SEを鳴らす
    /// </summary>
    public void PlaySE(AudioClip clip)
    {
        
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("SEをならしました");

        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }
}
