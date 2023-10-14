using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

public static class MathEX
{
    public static void FlipX(this Transform self){
        var a = self.transform.localScale;
        a.x *= -1;
        self.transform.localScale = a;
    }

    public static float Cross(this Vector2 self,Vector2 v){
        return self.x*v.y - self.y*v.x;
    }

    public static float Cross2(this Vector3 self,Vector3 v){
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
    public static Vector2 X(this Vector2 self,float v){
        return new Vector2(v,self.y);
    }
    public static Vector2 Y(this Vector2 self,float v){
        return new Vector2(self.x,v);
    }

    public static Vector2 Abs(this Vector2 vector){
        return new Vector2(Mathf.Abs(vector.x),Mathf.Abs(vector.y));
    }

    public static Vector3 Right(this Vector3 v){
        return new(v.y,-v.x,v.z);
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var rand = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public static Coroutine Delay(this MonoBehaviour monoBehaviour, System.Action action, float delay)
    {
        return monoBehaviour.StartCoroutine(CoroutineAction(action, delay));
    }

    public static Coroutine Interval(this MonoBehaviour monoBehaviour, System.Func<bool> action, float interval)
    {
        return monoBehaviour.StartCoroutine(CoroutineAction_Loop(action, interval));
    }

    private static IEnumerator CoroutineAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    private static IEnumerator CoroutineAction_Loop(System.Func<bool> action, float interval)
    {
        while (true)
        {
            if(action?.Invoke()??true) break;
            yield return new WaitForSeconds(interval);
        }
    }

    public static T ElementAtRandom<T>(this IEnumerable<T> self){
        return self.ElementAt(UnityEngine.Random.Range(0,self.Count()));
    }

    public static T ElementAtRandom<T>(this T[] self){
        return self[UnityEngine.Random.Range(0,self.Count())];
    }

    public static T ElementAtRandom<T>(this List<T> self){
        return self[UnityEngine.Random.Range(0,self.Count())];
    }
}

