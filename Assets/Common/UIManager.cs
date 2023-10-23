using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject timerPrefab;
    // Start is called from Common
    public void OnStartGame(){
        Instantiate(timerPrefab,transform);
    }
}
