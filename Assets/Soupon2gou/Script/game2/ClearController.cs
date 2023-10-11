using UnityEngine;
using System.Collections;

public class ClearController : MonoBehaviour
{
    public bool fall=false;

    void Update()
    {
        float dist = Vector3.Distance(new Vector3(0,0,0), transform.position);
        if (dist > 15f && !fall)
        {
            Debug.Log("落下");
            fall = true;
        }
    }
}
