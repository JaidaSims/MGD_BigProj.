using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForce : MonoBehaviour
{
    public float forceSpeed = 20;
    public bool destroyAfterUse = false;

    Vector3 dir;

    void start(){
        dir = this.transform.up;
            }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<Rigidbody>() != null){
                other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.up * forceSpeed, ForceMode.Impulse);
            
            if(destroyAfterUse){
                Destroy(this.gameObject, 0.25f);
            }
            }
        }
    }
}
