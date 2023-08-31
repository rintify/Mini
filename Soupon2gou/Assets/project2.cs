using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class project2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int [] a= new int [3];
        Debug.Log (a[0]);
        int  [] b= new int [2,2,2];
        for (int i=0; i<2;i++){
            for (int j=0; j<2;j++){
                for (int k=0;k<2;k++){
                    a[i,j,k]=i+j+k;
                    Debug.Log (a[i,j,k]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
