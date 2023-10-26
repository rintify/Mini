using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject lostPrefab,goalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<Skybox>().enabled = false;
        var goal = rooms.Shuffle();
        Instantiate(goalPrefab,goal[0].transform);
        for(int i = 1; i < rooms.Length; i ++){
            var a = Instantiate(lostPrefab,goal[i].transform);
            a.transform.Find("Door").GetComponent<Door>().goal = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
