using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;





    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("thisをインスタンスに入れる");
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("this.gameObjectの削除");
            Destroy(this.gameObject);
        }

    }

}   
