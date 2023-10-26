using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f; // 開く角度
    public float openSpeed = 2f; // 開くスピード
    public GameObject nobuLight;

    public bool isOpen = false;
    private Vector2 closedDir,openedDir;

    private void Start()
    {
        closedDir = transform.rotation.eulerAngles.y.Deg2Direction();
        openedDir = closedDir.Rotate(openAngle*Mathf.Deg2Rad);
    }


    private void Update()
    {
        var Dir = transform.rotation.eulerAngles.y.Deg2Direction();
        if(isOpen){
            //if()
            if(Dir.Dot(openedDir) < 0.99) 
                transform.Rotate(Vector3.up * openSpeed*Time.deltaTime);
            else{
                isOpen = false;
                Debug.Log("a");
            }
        }
    }

}
