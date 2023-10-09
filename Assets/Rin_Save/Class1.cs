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
    bool drawable = true;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        points = new List<Vector2>();
    }

    private void Update()
    {
        if(!drawable) return;
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (points.Count == 0 || (mousePosition - points.Last()).SqrMagnitude() > 0.25)
            {
                points.Add(mousePosition);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, mousePosition);
            }
        }
        if(Input.GetMouseButtonUp(0)){
            edgeCollider.points = points.ToArray();
            drawable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("sex");
    }
}
