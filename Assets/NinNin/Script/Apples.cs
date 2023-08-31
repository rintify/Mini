using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apples : MonoBehaviour
{
    public GameObject gameClearUI;
    public GameObject gameOverUI;
    public GameObject player;
    public bool isGameClear;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isGameClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && player.GetComponent<Player>().isGameOver != true)
        {
            gameClearUI.SetActive(true);
            isGameClear = true;

        }
    }
}
