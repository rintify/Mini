using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class coin_SE : MonoBehaviour
{
    AudioSource audioSource;
    public coin Coin;
    public float destroytime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Coin.SE)
        {
            Coin.SE = false;
            audioSource.Play();
            Destroy(this.gameObject,destroytime);

        }
    }
}
