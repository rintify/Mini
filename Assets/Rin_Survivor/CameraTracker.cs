using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform target; // 追跡対象のオブジェクト
    public float smoothSpeed = 0.125f;

    void Update()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = transform.position.XY(smoothedPosition);
    }
}
