using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Discord_Math : MonoBehaviour
{
    public SexyEncription sexyPrefab;
    [System.Serializable]public class Deck{public Sprite[] cards;}
    public Deck[] deck;
    Sprite[] sprites;
    private List<Sprite> cards;
    private List<SexyEncription> sexys = new();
    int n,m;
    private SexyEncription current = null;
    public GameObject batsuPrefab;
    public AudioClip bu,pinpon,pera;
    public bool listen = false;
    private int clearCount = 0;

    void Awake(){
        sprites = deck.ElementAtRandom().cards.Shuffle().ToList().GetRange(0,Common.RequiredLevel == 4 ? 6 : 1+Common.RequiredLevel).ToArray();
        var cards = new Sprite[sprites.Length*2];
        for(int i = 0; i < sprites.Length; i ++){
            cards[i*2] = sprites[i];
            cards[i*2 + 1] = sprites[i];
        }
        n = Mathf.FloorToInt(Mathf.Sqrt(cards.Length));
        m = Mathf.CeilToInt((float)cards.Length/n);
        this.cards = cards.ToList();
        this.cards.Shuffle();
        Common.StartGame(13,()=>{
            Common.EndGame(false);
        });

    }
    // Start is called before the first frame update
    void Start()
    {
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

        this.Delay(() => {
            foreach(var s in sexys) s.open();
            listen = true;
            Common.PlayOneShot(pera);
        },Common.RequiredLevel == 4 ? 6f :
        Common.RequiredLevel == 3 ? 4f : 2f);
        
    }

    public void notifyFliped(SexyEncription sexy){
        Common.PlayOneShot(pera);
        if(current == null) current = sexy;
        else{
            if(current.sexyprite == sexy.sexyprite){
                this.Delay(() => {Common.PlayOneShot(pinpon);},0.3f);
                
                var fin = current;
                current = null;
                clearCount ++;
                this.Delay(()=>{
                    Destroy(fin.gameObject);
                    Destroy(sexy.gameObject);
                    Debug.Log(cards.Count);
                    //クリア処理
                    if(clearCount >= sprites.Length){
                        Common.IsCleared = true;
                        this.Delay(()=>{
                            Common.EndGame();
                        },0.3f);
                    }
                },0.5f);
            }
            else{
                listen = false;
                this.Delay(() => {Common.PlayOneShot(bu);},0.3f);
                Instantiate(batsuPrefab,sexy.transform);
                this.Delay(()=>{
                    Common.EndGame(false);
                },0.5f);
            }
        }
    }

}
