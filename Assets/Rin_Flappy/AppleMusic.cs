using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleMusic : MonoBehaviour
{
    public float vx = 5;
    float vy = 0;
    public float gravity = 10f,power = 20f,vymax = 3f;
    float gravityMax;
    Camera camera;
    public ParticleSystem pa;
    EX.Virgin over,clear;
    GameObject buddy;
    public AudioClip guki;
    public AudioSource pushu;

    // Start is called before the first frame update
    void Start()
    {
        gravityMax = gravity;
        gravity = 0;
        buddy = GameObject.Find("buddy");
        camera = Camera.main;
        pa.Stop();
        over = new EX.Virgin(() => {
            Common.PlayOneShot(guki);
            Common.EndGame(false);
        });
        clear = new EX.Virgin(() => {
            Common.EndGame(true);
        });
        Common.StartGame(Mathf.CeilToInt(
            buddy.transform.position.x-transform.position.x + 0.5f)
            ,() => {Common.EndGame(true);}
        );
    }

    // Update is called once per frame
    void Update()
    {
        if(!over.isVirgin || !clear.isVirgin) return;

        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if(transform.position.y < -screenBounds.y-0.5 || transform.position.y > screenBounds.y+0.5 ) over.Break();
        if(transform.position.x > buddy.transform.position.x - 5) clear.Break();

        var pos = transform.position;

        gravity += gravityMax/1f*Time.deltaTime;
        if(gravity > gravityMax) gravity = gravityMax;

        vy -= gravity*Time.deltaTime;
        if(vy < -vymax) vy = - vymax;
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)){
            vy += power*Time.deltaTime;
            if(vy > vymax) vy = vymax;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){
            pushu.Play();
            pa.Play();
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)){
            pushu.Stop();
            pa.Stop();
        }

        pos.y += vy*Time.deltaTime;
        pos.x += vx*Time.deltaTime;

        transform.position = pos;

        camera.transform.position =
        camera.transform.position.X(pos.x);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        over.Break();
    }

    
}
