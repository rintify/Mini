using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSound : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;
    public GameObject apple;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayMethod", 2f);
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posi = apple.transform.position;
        
        if(posi.y == 3.8)
        {
            audioSource.PlayOneShot(sound1);
            Debug.Log("audio");
        }
    }
}
