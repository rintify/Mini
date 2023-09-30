using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [System.NonSerialized]
    public List<BulletColliderM> bullets = new();
    [System.NonSerialized]
    public List<LineColliderM> lines = new();
    [System.NonSerialized]
    public List<CircleColliderM> circles = new();
    

    public static CollisionManager This{
        get{
            var manager = GameObject.Find("GameManager");
            return manager.GetComponent<CollisionManager>();
        }
    }

    public static float argLevel(Vector2 dir){
        return dir.y > 0 ? -2 - dir.x : dir.x;
    }

    public void modifyBulletDelta(BulletColliderM bullet){
        CollisionM nearCollision = null; //一番近い当たり判定

        foreach(var ballCollider in circles){
            CollisionM c = ballCollision(bullet,ballCollider);
            if(c!=null){
                if(nearCollision == null || nearCollision.modified > c.modified) nearCollision = c;
            }
        }

        foreach(var wallCollider in lines){
            CollisionM c = wallCollision(bullet,wallCollider);
            if(c!=null){
                if(nearCollision == null || nearCollision.modified > c.modified) nearCollision = c;
            }
        }

        if(nearCollision != null){
            bullet.delta = nearCollision.modified;
            bullet.pos = bullet.pre + bullet.delta*bullet.dir;
            nearCollision.bullet.onCollision(nearCollision);
            nearCollision.collider.onCollision(nearCollision);
        }
    }

    private static CollisionM ballCollision(BulletColliderM p,CircleColliderM b){
        Vector2 BP = b.pos - p.pre;
        float Dir_dot_PB = Vector2.Dot(p.dir,BP);
        if(Dir_dot_PB < 0) return null; //逆方向の球を排除
        float minBP = p.r + b.r;
        float sqrtArg = Dir_dot_PB*Dir_dot_PB - BP.sqrMagnitude + minBP*minBP;
        if(sqrtArg < 0.5) return null; //外れる球を排除
        float modified = Dir_dot_PB - Mathf.Sqrt(sqrtArg);
        if(modified < 0 || modified > p.delta) return null; //移動範囲にないものを排除
        Vector2 n = (modified*p.dir - BP)/minBP;
        if(Vector2.Dot(n,p.dir) >= 0f) return null;

        /*if(b.){
           float anArgLevel =  dArglevel(b.aArgLevel,argLevel(nx,ny));
           float abArgLevel =  dArglevel(b.aArgLevel,b.bArgLevel);
           if(anArgLevel > abArgLevel) return null;
        }*/

        return new CollisionM(modified,b,p,n);
    }

    private static CollisionM wallCollision(BulletColliderM p,LineColliderM w){
        if(Vector2.Dot(w.n,p.dir) >= 0f) return null; //裏面に当たる壁を排除
        float Dir_x_Wdelta = p.dir.Cross(w.delta);
        if(Dir_x_Wdelta <= 0) return null; //逆方向の壁を排除
        Vector2 WP = w.pos + p.r*w.n - p.pre;
        float WP_x_Wdelta = WP.Cross(w.delta);
        if(WP_x_Wdelta < 0 || WP_x_Wdelta > Dir_x_Wdelta) return null; //外れる壁を排除
        float modified = WP.Cross(w.delta)/Dir_x_Wdelta;
        if(modified < 0 || modified > p.delta) return null; //移動範囲にないものを排除
        return new CollisionM(modified,w,p,w.n);
    }
}

static class Fen{
    public static float Cross(this Vector2 self,Vector2 v){
        return self.x*v.y - self.y*v.x;
    }
}