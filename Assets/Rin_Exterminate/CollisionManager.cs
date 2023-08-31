using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [System.NonSerialized]
    public List<BalletCollider> ballets = new();
    [System.NonSerialized]
    public List<WallCollider> walls = new();

    void Start()
    {
        
    }

    void FixedUpdate() {
        
    }

    public static CollisionManager This{
        get{
            var manager = GameObject.Find("GameManager");
            return manager.GetComponent<CollisionManager>();
        }
    }
}
