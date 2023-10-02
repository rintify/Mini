
using UnityEngine;

public static class MathEX
{
    public static float Cross(this Vector2 self,Vector2 v){
        return self.x*v.y - self.y*v.x;
    }
    
    public static Vector2 Deg2Direction(this float degree){
        float rad = degree*Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad),Mathf.Sin(rad));
    }

    public static Vector3 XY(this Vector3 self,Vector2 v){
        return new Vector3(v.x,v.y,self.z);
    }
    public static Vector3 XY(this Vector3 self,float x,float y){
        return new Vector3(x,y,self.z);
    }
    public static Vector3 X(this Vector3 self,float v){
        return new Vector3(v,self.y,self.z);
    }
    public static Vector3 Y(this Vector3 self,float v){
        return new Vector3(self.x,v,self.z);
    }
    public static Vector3 Z(this Vector3 self,float v){
        return new Vector3(self.x,self.y,v);
    }
    public static Vector2 ZX(this Vector2 self,float v){
        return new Vector2(v,self.y);
    }
    public static Vector2 Y(this Vector2 self,float v){
        return new Vector2(self.x,v);
    }

    public static Vector2 Abs(this Vector2 vector){
        return new Vector2(Mathf.Abs(vector.x),Mathf.Abs(vector.y));
    }
}

