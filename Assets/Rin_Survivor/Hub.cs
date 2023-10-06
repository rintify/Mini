using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    private Camera mainCamera;
    public float rotationSpeed = 20f;
    private Vector3 screenBounds;
    void Start(){
        mainCamera = Camera.main;

        // カメラの表示領域の境界を取得
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    private void kLateUpdate()
    {
        // オブジェクトの位置を表示領域の境界内に制限
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 , screenBounds.y);
        transform.position = viewPos;
    }
}
