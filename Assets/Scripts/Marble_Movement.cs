using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Marble_Movement : MonoBehaviour
{

    Vector3 dir;
    Rigidbody rb;

    public bool debug = true;
    public float speed = 10;
    public Transform arrowIndicator;

    public GameObject NextScreen;
    public TextMeshProUGUI Time;

    //public bool FireworksBool = false;
    ParticleSystem RightFireworks;
    ParticleSystem LeftFireworks;
    public GameObject RFireworks;
    public GameObject LFireworks;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();    
        NextScreen.SetActive(false);

        RightFireworks = RFireworks.GetComponent<ParticleSystem>(); 
        LeftFireworks = LFireworks.GetComponent<ParticleSystem>();
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

    void LateUpdate(){
        arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 scale = Vector3.one;
        scale.z = dir.magnitude;
        arrowIndicator.localScale = scale;
    }

    void FixedUpdate(){
        rb.AddForce(dir * speed);
    }

    // Scene currentScene = SceneManager.GetActiveScene ();
    // string sceneName = currentScene.name;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("NextLevel")){
            NextScreen.SetActive(true);

           // if(sceneName == "Level4"){ //If in lvl 4, set off fireworks! Otherwise, no fireworks
                RightFireworks.Play();
                LeftFireworks.Play();
           // }
            
           // FireworksBool = true;
        }
        
        if(other.CompareTag("Respawn")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("I've died!");
        }
    }

    public void Next(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    
}
