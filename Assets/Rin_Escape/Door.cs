using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f; // 開く角度
    public float openSpeed = 2f; // 開くスピード
    public GameObject nobuLight;
    public bool goal = false;

    public bool isOpen = false;
    private Vector2 closedDir,openedDir;

    EX.Virgin onOpen;

    private void Start()
    {
        closedDir = transform.rotation.eulerAngles.y.Deg2Direction();
        openedDir = closedDir.Rotate(openAngle*Mathf.Deg2Rad);
        onOpen = new(() => {
            if(goal){
                Debug.Log("afa");
                Camera.main.GetComponent<Skybox>().enabled = true;
                Common.IsCleared = true;
                this.Delay(() => {
                    Common.EndGame();
                },1.5f);
            }
        });
    }


    private void Update()
    {
        var Dir = transform.rotation.eulerAngles.y.Deg2Direction();
        if(isOpen){
            onOpen.Break();
            if(Dir.Dot(openedDir) < 0.99) 
                transform.Rotate(Vector3.up * openSpeed*Time.deltaTime);
            else{
                isOpen = false;
            }
        }
    }

}
