using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootinggamemaneger : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemy = 5;
        new Vector3 point;
    public GameObject target;
    void Start()
    {
        //Camera.main.gameObject;
        for (int i = 0; i < enemy; i++)
        {
            point =new Vector3(Random.Range(-8f, 8f),3.5f,0f);
            Instantiate(target, point, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
