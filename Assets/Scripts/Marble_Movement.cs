using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Marble_Movement : MonoBehaviour
{
    //MOVEMENT STUFF
    Vector3 dir;
    Rigidbody rb;

    public bool debug = true;
    public float speed = 10;

    Vector3 calibrateDir;

    public Transform arrowIndicator;
    ////////////////

    //END OF LVL SCREEN
    public GameObject NextScreen;
    public TextMeshProUGUI Time;
    ///////////////

    //public bool FireworksBool = false;

    //FIREWORKDS
    ParticleSystem RightFireworks;
    ParticleSystem LeftFireworks;
    public GameObject RFireworks;
    public GameObject LFireworks;
    ////////////////

    //VARIOUS 
    public float jumpSpeed = 5f;
    public int score = 0;
    public GameObject JumpButton;
    ////////////////
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();    
        NextScreen.SetActive(false);

        RightFireworks = RFireworks.GetComponent<ParticleSystem>(); 
        LeftFireworks = LFireworks.GetComponent<ParticleSystem>();

        Calibrate();
        startPos = this.transform.position;

        JumpButton.SetActive(canJump); //if can jump, show button 
    }

    // Update is called once per frame
    void Update()
    {
        // dir = Vector3.zero; //New Vector3(0, 0, 0)

        dir.x = Input.acceleration.x - calibrateDir.x;
        dir.z = Input.acceleration.y - calibrateDir.z;

        if(debug) {
            Debug.DrawRay(this.transform.position, dir * 2, Color.black, 0.5f);
        }

        
    }

    void LateUpdate(){
        arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 scale = Vector3.one;
        scale.z = dir.magnitude;
        arrowIndicator.localScale = scale;

        if(this.transform.position.y <= -1){
            ResetPosition();
        }
    }

    Vector3 startPos;
    
    void ResetPosition(){
        this.transform.position = startPos;
        rb.velocity = Vector3.zero;
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

        if(other.gameObject.CompareTag("Coin")){
            score += 50;
            Destroy(other.gameObject);
            //Play coin audio??
        }

           if(other.gameObject.name == "JumpPowerUp"){
                canJump = true; 
                JumpButton.SetActive(canJump);
                Destroy(other.gameObject);  
        }
 
    }

    public void Next(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void Calibrate(){
        calibrateDir.x = Input.acceleration.x;
        calibrateDir.z = Input.acceleration.y;
        Debug.Log("Calibrated Dir = " + calibrateDir);
    }

    public void Jump(){
    if(isGrounded && canJump){
         rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    }

    bool isGrounded = true;
    bool canJump = false;

    void OnCollisionEnter(){       
        isGrounded = true;
    }

    void OnCollisionExit(){
        isGrounded = false;
    }
}
