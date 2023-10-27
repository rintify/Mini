using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Class1 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector2> points;
    Rigidbody2D rb;
    public bool drawable = true;
    public AudioClip hati;
    Dog dog;
    EX.Virgin startAtack;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        points = new List<Vector2>();

        edgeCollider.enabled = false;
        rb.isKinematic = true;

        dog = GameObject.Find("Dog").GetComponent<Dog>();
        dog.GetComponent<Rigidbody2D>().isKinematic = true;

        Common.StartGame(8,()=>{
            startAtack.Break();
        });

        startAtack = new(() => {
            edgeCollider.points = points.ToArray();
            drawable = false;
            edgeCollider.enabled = true;
            rb.isKinematic = false;
            dog.GetComponent<Rigidbody2D>().isKinematic = false;
            Common.PlayOneShot(hati);

            Common.RestartTimer(7, () => {Common.EndGame(true);});
        });
    }

    private void Update()
    {
        if(!drawable) return;
        if (Input.GetMouseButton(0))
        {
            var mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = transform.InverseTransformPoint(mouseWP);

            if (points.Count == 0 || 
                (mousePosition - points.Last()).SqrMagnitude() > 0.25 &&
                !Physics2D.Linecast(transform.TransformPoint(points.Last()), mouseWP)
            ){
                points.Add(mousePosition);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, mousePosition);
            }
        }
        if(Input.GetMouseButtonUp(0)){
            startAtack.Break();
        }
    }

}
