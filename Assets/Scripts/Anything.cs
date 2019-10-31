using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anything : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

// test
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            this.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
}
