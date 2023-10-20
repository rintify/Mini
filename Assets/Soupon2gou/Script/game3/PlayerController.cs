using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isStop=false;
    float startY;

    public bool StageClear=false;
    public float rotate_speed =1f;
    private AudioSource audioSource = null;
    [Header("クリアSE")] public AudioClip SwordSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startY = transform.position.y;
        Common.StartGame(8, () => { Common.EndGame(false); });
    }

    // Update is called once per frame
    void Update()
    {
        bool verticalKey = Input.GetKey(KeyCode.Space);
        if (verticalKey)
        {
            Stop();
        }

        
        if (isStop) return ;
        //毎フレーム回転させる
        transform.Rotate(new Vector3(0, rotate_speed, 0));

        //少しずつ下へ
        //transform.Translate(0, -0.005f, 0);

        //下まで行ったら上に戻る
        if (1 > transform.position.y)
        {
            Vector3 pos = transform.position;  
            pos.y = startY;
            transform.position = pos;
        }
    }

    //Colliderの当たり判定があった時に呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.name.Equals("Clear"))
        {
            Debug.Log("game3クリア!");
            StartCoroutine(Fin_C());
        }
    }

    IEnumerator Fin_C()
    {
        //音楽を鳴らす
        audioSource.PlayOneShot(SwordSE);
        Debug.Log("ステージクリアSEをならしました");

        //終了まで待機
        yield return new WaitWhile(() => audioSource.isPlaying);

        Common.EndGame(true);
    }

    public void Stop()
    {
        isStop = true;
        //落下させる
        GetComponent<Rigidbody>().isKinematic = false;
    }

    /*public void Retry()
    {
        SceneManager.LoadScene("game3");
    }
    */
}
