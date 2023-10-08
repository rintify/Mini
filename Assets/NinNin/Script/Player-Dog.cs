using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject gameOverUI;
    public bool isGameOver;
 
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x > -8)
                this.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x < 8)
                this.transform.position += Vector3.right * speed * Time.deltaTime;
        }  
    }

    void OnCollisionEnter2D(Collision2D collision)
  {
            if(collision.gameObject.CompareTag("Apple"))
            { 
                gameOverUI.SetActive(true);
                isGameOver = true;
            }
  }
}
