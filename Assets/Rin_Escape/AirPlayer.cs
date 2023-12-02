using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlayer : MonoBehaviour
{
    public float speed = 3f;
    public AudioClip foot1,foot2;
    float dS = 0;
    Rigidbody rb;

    string wPressed = null;
    bool run = false;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 0.6f, Ysensityvity = 0.6f;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RenderSettings.ambientLight = new Color(0.05f,0.05f,0.05f,0.05f);
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        Common.StartGame(15,()=>{
            Common.EndGame(false);
        });
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        if(Input.GetKeyDown(KeyCode.W)){
            if(wPressed == null){
                var guid = Guid.NewGuid().ToString();
                wPressed = guid;
                this.Delay(() => {
                    if(wPressed == guid) wPressed = null;
                },0.5f);
            }
            else{
                run = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.W)){
            run = false;
        }

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        Ray ray = Camera.main.ViewportPointToRay(0.5f*Vector2.one);
        if(Physics.Raycast(ray, out RaycastHit hit, 2.5f)){
            var door = hit.collider.gameObject.GetComponent<Door>();
            if(door){
                if(!door.nobuLight.activeInHierarchy){
                    door.nobuLight.SetActive(true);
                    this.Delay(()=>{
                        if(door.nobuLight.activeInHierarchy)door.nobuLight.SetActive(false);
                    },0.5f);
                }
                if(Input.GetMouseButtonDown(0)) door.isOpen = true;
            }
        }

    }

    bool foot = false;
    private void FixedUpdate()
    {
        var dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if(dir == Vector2.zero) return;

        var ds = (run ? speed * 2.5f : speed * 2f) * Time.fixedDeltaTime;
        dS += ds;
        if (dS > 3f)
        {
            Common.PlayOneShot(foot ? foot1 : foot2);
            foot = !foot;
            dS -= 3f;
        }

        var move = ds * dir;

        Vector3 forwardWithoutY = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 rightWithoutY = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

        Vector3 newPosition = transform.position + forwardWithoutY * move.y + rightWithoutY * move.x;

        rb.MovePosition(newPosition);
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX,minX,maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

}
