using System;
using Unity.VisualScripting;

public abstract class ColliderM{
    public Action<CollisionM> onCollision;

    protected ColliderM(Action<CollisionM> onCollision){
        this.onCollision = onCollision;
    }
}