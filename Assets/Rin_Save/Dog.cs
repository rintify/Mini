using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Cry cryPrefab;
    EX.Virgin over;
    EX.Intervalist cryer;
    public AudioClip doggy;
    // Start is called before the first frame update
    void Start()
    {
        over = new(()=>{
            Common.EndTimer();
            Common.IsCleared = false;
            this.Delay(()=>{
                Common.EndGame();
            },1.5f);
            float b = 1f;
            cryer = new(()=>{
                var a = Instantiate(cryPrefab,transform);
                a.speed = new(b*1f*Time.deltaTime,0);
                b *= -1;
                if(b == -1) Common.PlayOneShot(doggy);
            },0.45f,1f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        cryer?.Update();
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if(transform.position.y < -screenBounds.y) over.Break();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var bee = other.gameObject.GetComponent<Bee>();
        if(bee){
            Debug.Log("die");
            over.Break();
        }
    }
}
