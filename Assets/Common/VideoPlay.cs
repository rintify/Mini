using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer video;

    void Start()
    {
        video.url = "https://cas-ru.com/share/cas_background.mp4";
        video.prepareCompleted += PrepareCompleted;
        video.errorReceived += ErrorReceived;
    }

    // エラー発生時に呼ばれる
    void ErrorReceived(UnityEngine.Video.VideoPlayer vp, string message)
    {
        Debug.LogWarning($"動画の読み込みに失敗しました. message:{message}");
        vp.errorReceived -= ErrorReceived;
        vp.prepareCompleted -= PrepareCompleted;
        // エラー時処理
    }

    // 動画の読み込みが完了したら呼ばれる
    void PrepareCompleted(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("動画ロード完了");
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
    }
}
