using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble_Movement : MonoBehaviour
{

    Vector3 dir;
    Rigidbody rb;

    public bool debug = true;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        // dir = Vector3.zero; //New Vector3(0, 0, 0)

        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        if(debug) {
            Debug.DrawRay(this.transform.position, dir * 2, Color.black, 0.5f);
        }
    }

    void FixedUpdate(){
        rb.AddForce(dir * speed);
    }
}
