using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject timerPrefab;
    GameObject timer;
    // Start is called from Common
    public void OnStartGame(){
        timer = Instantiate(timerPrefab,transform);
    }

    // Start is called from Common
    public void OnBreakGame(){
        Destroy(timer);
    }
}
