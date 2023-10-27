using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TypeMonster : MonoBehaviour
{
    public Sprite[] sprites;
    int sp_i = 0;
    EX.Intervalist anime;
    public SpriteRenderer renn;
    float time;
    Action state;
    public Twitter twittePrefab;
    public Canvas canvas;
    public ParticleSystem pa;
    public AudioClip ban,bowa;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = new Color(0.1f,0.08f,0.08f,0.1f);
        anime = new(() => {
            renn.sprite = sprites[sp_i%sprites.Length];
            sp_i ++;
            
        },0.1f);

        state = () => {
            transform.position = new(0,-10*Mathf.Pow(2,-2f*time)+5f);
            if(time > 1){
                time = 0;
                state = () => {
                    transform.position -= 10f*Time.deltaTime*Vector3.up;
                    transform.localScale = (1f + 1.6f*time)*Vector3.one;
                    if(transform.position.y < 0){
                        var timeLimit = Common.RequiredLevel == 4 ? 15 :
                            Common.RequiredLevel == 3 ? 12 :
                            10;
                        Common.StartGame(
                            timeLimit,()=>{
                            Common.EndGame(false);
                        });
                        var a = Instantiate(twittePrefab,canvas.transform);
                        Common.PlayOneShot(bowa);
                        a.timeLimit = timeLimit;
                        a.monster = this;
                        time = 0;
                        state = () => {
                            transform.position = transform.position.X(Mathf.Sin(time*3.14f)*2.5f);
                        };
                    }
                };
            }
        };
    }

    public void die(){
        pa.Play();
        Common.PlayOneShot(ban);
        renn.color=Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        anime.Update();
        state();
        time += Time.deltaTime;
    }
}
