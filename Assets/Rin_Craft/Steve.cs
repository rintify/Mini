using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class Steve : MonoBehaviour
{
    public float speed = 3f;
    public AudioClip foot1,foot2,ton,gan;
    float dS = 0;
    Rigidbody rb;
    public float jump;

    string wPressed = null;
    bool run = false;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;
    
    public Color ambient;
    public float gravity = -10;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    public GameObject blockPrefab;

    Vector3[] question = new Vector3[]{
        new(0,0,0),
        new(0,1,0),
        new(1,1,0)
    };
    List<GameObject> answer = new();

    EX.Virgin clear;

    void GenerateQuestion(){
        var min = SearchBottomRight(question);
        for (int i = 0; i < question.Length; i++){
            question[i] -= min;
            Instantiate(blockPrefab, question[i] + 0.5f*Vector3.one, Quaternion.identity);
        }
    }

    Vector3 SearchBottomRight(Vector3[] vs){
        var min = vs[0];
        foreach(var v in vs){
            if(v.y < min.y) min = v;
            else if(v.y == min.y){
                if(v.x < min.x) min = v;
                else if(v.z == min.z){
                    if(v.z < min.z) min = v;
                }
            }
        }
        return min;
    }

    void CheckAnswer(){
        if(answer.Count != question.Length) return;
        Debug.Log("check");
        var ain = answer.Select(a => a.transform.position).ToArray();
        var min = SearchBottomRight(ain);
        foreach(var q in question){
            var index = Array.IndexOf(ain,q + min);
            if(index == -1) return;
        }
        clear.Break();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestion();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        Common.StartGame(15,()=>{
            Common.EndGame(false);
        });
        rb = GetComponent<Rigidbody>();

        RenderSettings.ambientLight = ambient;
        Physics.gravity = gravity*Vector3.up;
        Cursor.lockState = CursorLockMode.Locked;

        clear = new(() => {
            Common.EndGame(true);
        });
    }


    // Update is called once per frame
    void Update()
    {
        if(!clear.isVirgin) return;
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

        if(Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ViewportPointToRay(0.5f*Vector2.one);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                var normal = hit.normal;
                var genPos = hit.point + normal*0.99f;
                Vector3 pos = new(
                    Mathf.Floor(genPos.x) + 0.5f,
                    Mathf.Floor(genPos.y) + 0.5f,
                    Mathf.Floor(genPos.z) + 0.5f
                );
                // レイが何かのオブジェクトにヒットした場合、その位置にオブジェクトを生成
                var b = Instantiate(blockPrefab, pos, Quaternion.identity);
                b.GetComponent<Tag>().value = 1;
                answer.Add(b);
                Common.PlayOneShot(ton);
                CheckAnswer();
            }
        }

        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ViewportPointToRay(0.5f*Vector2.one);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f) && hit.collider.gameObject.GetComponent<Tag>()?.value == 1)
            {
                answer.Remove(hit.collider.gameObject);
                // レイが何かのオブジェクトにヒットした場合、その位置にオブジェクトを生成
                Destroy(hit.collider.gameObject);
                Common.PlayOneShot(gan);
                CheckAnswer();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(jump*Vector3.up);
        }

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

    }

    bool foot = false;
    bool isGrounded;
    private void FixedUpdate()
    {
        if(!clear.isVirgin) return;
        var preIsGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position - 1.7f*Vector3.up, Vector3.down, 0.3f);
        if(!preIsGrounded && isGrounded){
            if(transform.position.y < 2) Common.PlayOneShot(foot1);
            dS = 0;
        }   

        var dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if(dir == Vector2.zero) return;

        var ds = (run ? speed * 2 : speed) * Time.fixedDeltaTime;
        if(isGrounded){
            dS += ds;
            if (dS > 3f)
            {
                if(transform.position.y < 2) Common.PlayOneShot(foot ? foot1 : foot2);
                foot = !foot;
                dS -= 3f;
            }
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
