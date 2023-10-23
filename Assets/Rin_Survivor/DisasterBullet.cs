using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterBullet : MonoBehaviour
{
    MathEX.Delayer a;
    public float speed;
    Vector2 dir;
    public Disaster parent;
    // Start is called before the first frame update
    void Start()
    {
        a = new(() => {Destroy(this.gameObject);}, 2f);
        dir = transform.localEulerAngles.z.Deg2Direction();
    }

    // Update is called once per frame
    void Update()
    {
        a.Update();
        transform.position += (Vector3)(speed*Time.deltaTime*dir);
    }

    private void OnDestroy() {
        parent.refrectedBullets.Remove(this);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<PlayHub>() != null){
            Debug.Log("player");
            Common.PlayOneShot(other.GetComponent<PlayHub>().ban);
            Destroy(other.gameObject);
            this.Delay(()=>Common.EndGame(false),0.4f);
        }
        else if(other.GetComponent<Disaster>() != null){
            Debug.Log("enemy");
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<Orgasm>() != null){
            var a = other.GetComponent<Orgasm>();
            dir = a.Dir.Right();
            dir *= -Mathf.Sign(a.parent.rotationSpeed);
            this.transform.rotation = dir.Quaternion();
            parent.refrectedBullets.Add(this);
        }
    }
}
