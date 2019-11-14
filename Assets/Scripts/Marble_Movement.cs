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
    public TextMeshProUGUI TimeTxt;
    public TextMeshProUGUI TimeTxtEnd;
    public GameObject TimeTxtUI;
    public GameObject TimeTxtGO;
    ///////////////


    //public bool FireworksBool = false;

    //FIREWORKDS
    ParticleSystem RightFireworks;
    ParticleSystem LeftFireworks;
    public GameObject RFireworks;
    public GameObject LFireworks;
    ////////////////

    
    //AUDIO
    //To be done in near future

    ///////////////


    //VARIOUS 
    public float jumpSpeed = 5f;
    public int score = 0;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI ScoreEnd;
    public GameObject ScoreTxtUI;
    public GameObject ScoreGO;
    public GameObject JumpButton;

    float timer = 0f;
    ////////////////


    //GATE
    bool hasKey = false;
    ////////////////

    //FLYTEXT
    public GameObject keyMessage;
    public GameObject scoreMessage;
    public GameObject jumpMessage;
    //////////////

    //CAMERA
    public Camera mainCam;
    //private Camera mainCam;
    ///////////////
    

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

        keyMessage.SetActive(false);
        scoreMessage.SetActive(false);
        jumpMessage.SetActive(false);

       // mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TimeTxt.text = timer.ToString("F0");

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
           // Time.timeScale = 0;

            //Time & Score
            TimeTxtEnd.text = timer.ToString("F0");
            ScoreEnd.text = score.ToString();

            ScoreGO.SetActive(false);
            ScoreTxtUI.SetActive(false);
            TimeTxtGO.SetActive(false);
            TimeTxtUI.SetActive(false);

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
            StartCoroutine(FlyScore());
            score += 50;
            Score.text = score.ToString();
            Destroy(other.gameObject);
            //Play coin audio??
        }

           if(other.gameObject.name == "JumpPowerUp"){
                StartCoroutine(FlyJump());
                canJump = true; 
                JumpButton.SetActive(canJump);
                Destroy(other.gameObject);  
        }
            
        if(other.gameObject.CompareTag("Key")){
            StartCoroutine(FlyKey());
            hasKey = true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.name == "Gate"){
            if(hasKey){
                Destroy(other.gameObject);
            }
        }

        if(other.gameObject.CompareTag("CustomCam")){
            mainCam.gameObject.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }

        //  if(other.gameObject.CompareTag("NormalCam")){
        //      mainCam.gameObject.SetActive(true);
            
        //  }
 
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("CustomCam")){
            mainCam.gameObject.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    public void Next(){
        Time.timeScale = 1;
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

    public void Teleport(){
        Debug.Log("Teleporting dir = " + dir);
        //Debug.Log("dir = " + dir);
        this.transform.Translate(arrowIndicator.forward * 3, Space.World);
    }

    bool isGrounded = true;
    bool canJump = false;

    void OnCollisionEnter(){       
        isGrounded = true;
    }

    void OnCollisionExit(){
        isGrounded = false;
    }

    //Will allow text to flash quickly accross the screen
    IEnumerator FlyKey(){
        keyMessage.SetActive(true);
        yield return new WaitForSeconds(.9f);
        keyMessage.SetActive(false);        
    }

    IEnumerator FlyScore(){
        scoreMessage.SetActive(true);
        yield return new WaitForSeconds(.9f);
        scoreMessage.SetActive(false);        
    }

    IEnumerator FlyJump(){
        jumpMessage.SetActive(true);
        yield return new WaitForSeconds(.9f);
        jumpMessage.SetActive(false);        
    }
}
