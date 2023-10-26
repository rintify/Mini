using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMonster : MonoBehaviour
{
    public Sprite[] sprites;
    EX.Intervalist anime;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        anime = new(() => {

        },3,1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
