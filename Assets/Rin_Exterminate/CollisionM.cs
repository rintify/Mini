using UnityEngine;

public class CollisionM{
    public float modified;
    public ColliderM bullet,collider;
    public Vector2 normal;

    public CollisionM(float modified,ColliderM bullet, ColliderM collider, Vector2 normal)
    {
        this.modified = modified;
        this.bullet = bullet;
        this.collider = collider;
        this.normal = normal;
    }
}