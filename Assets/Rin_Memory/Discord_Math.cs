using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Discord_Math : MonoBehaviour
{
    public SexyEncription sexyPrefab;
    public Sprite[] sprites;
    private List<Sprite> cards;
    private List<SexyEncription> sexys = new();
    int n,m;
    public Text title;
    private SexyEncription current = null;
    public GameObject batsuPrefab;
    public AudioClip bu,pinpon;
    private AudioSource source;
    public bool listen = false;

    void Awake(){
        var cards = new Sprite[sprites.Length*2];
        for(int i = 0; i < sprites.Length; i ++){
            cards[i*2] = sprites[i];
            cards[i*2 + 1] = sprites[i];
        }
        n = Mathf.FloorToInt(Mathf.Sqrt(cards.Length));
        m = Mathf.CeilToInt((float)cards.Length/n);
        this.cards = cards.ToList();
        this.cards.Shuffle();
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        for(int i = 0; i < n; i ++){
            for(int j = 0; j < m; j ++){
                if(cards.Count < 1) continue;
                var sexy = Instantiate(sexyPrefab,this.transform);
                sexys.Add(sexy);
                var sex = sexy.gameObject.GetComponent<RectTransform>();
                var card = cards[0];
                cards.RemoveAt(0);
                sexy.set(card,this);
                var size = sex.sizeDelta;
                sex.anchoredPosition = new(-size.x*m/2 + (size.x + 10)*j, -size.y*n/2 + (size.y+10)*i);
            }
        }

        StartCoroutine(DelayedAction());
        
    }

    public void notifyFliped(SexyEncription sexy){
        if(current == null) current = sexy;
        else{
            if(current.sexyprite == sexy.sexyprite){
                source.PlayOneShot(pinpon);
                
                var fin = current;
                current = null;
                this.StartCoroutine(()=>{
                    Destroy(fin.gameObject);
                    Destroy(sexy.gameObject);
                },0.3f);
            }
            else{
                source.PlayOneShot(bu);
                Instantiate(batsuPrefab,sexy.transform);
            }
        }
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(2f); // 3秒の遅延
        foreach(var s in sexys) s.open();
        title.text = "";
        listen = true;
    }
}
