using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public EnemyHubLv1 prefab;
    EX.Intervalist a;
    public float r = 13;
    public float interval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        a = new(go,interval);
    }

    void go(){
        var ai = Instantiate(prefab); // Prefabをインスタンス化
        ai.transform.position = r*Random.Range(1f,1.2f)*Random.Range(0f,360f).Deg2Direction();
        ai.rotationSpeed *= Random.Range(0,2) == 0 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        a.Update();
    }
}
