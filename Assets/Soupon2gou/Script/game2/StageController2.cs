using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController2 : MonoBehaviour
{
    GameObject Ob1,Ob2,Ob3;

    ClearController script1, script2, script3;

    bool f1 = false, f2 = false, f3 = false;
    public bool ClearStage2=false;

    private void Start()
    {
        Common.StartGame(8, () => { Common.EndGame(ClearStage2); });

        Ob1 = GameObject.Find("Cylinder");
        Ob2 = GameObject.Find("Sphere");
        Ob3 = GameObject.Find("Cube");
        script1 = Ob1.GetComponent<ClearController>();
        
        script2 = Ob2.GetComponent<ClearController>();
        
        script3 = Ob3.GetComponent<ClearController>();
        

    }


    // Update is called once per frame
    void Update()
    {
        
        f1 = script1.fall;
        f2 = script2.fall;
        f3 = script3.fall;

        if (f1 && f2 && f3 && !ClearStage2)
        {
            Debug.Log("ステージ2クリア！");
            ClearStage2 = true;

            //クリアしたらどうする？
            //GameManager.instance.score++;

        }
    }
}
