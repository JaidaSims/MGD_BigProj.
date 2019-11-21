using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //FIREWORKDS
    ParticleSystem RightFireworks;
    ParticleSystem LeftFireworks;
    public GameObject RFireworks;
    public GameObject LFireworks;

    ParticleSystem RightFireworks2;
    ParticleSystem LeftFireworks2;
    public GameObject RFireworks2;
    public GameObject LFireworks2;

    ParticleSystem RightFireworks3;
    ParticleSystem LeftFireworks3;
    public GameObject RFireworks3;
    public GameObject LFireworks3;

    ParticleSystem RightFireworks4;
    ParticleSystem LeftFireworks4;
    public GameObject RFireworks4;
    public GameObject LFireworks4;
    ////////////////


    // Start is called before the first frame update
    void Start()
    {
        RightFireworks = RFireworks.GetComponent<ParticleSystem>(); 
        LeftFireworks = LFireworks.GetComponent<ParticleSystem>();

        RightFireworks2 = RFireworks2.GetComponent<ParticleSystem>(); 
        LeftFireworks2 = LFireworks2.GetComponent<ParticleSystem>();

        RightFireworks3 = RFireworks3.GetComponent<ParticleSystem>(); 
        LeftFireworks3 = LFireworks3.GetComponent<ParticleSystem>();

        RightFireworks4 = RFireworks4.GetComponent<ParticleSystem>(); 
        LeftFireworks4 = LFireworks4.GetComponent<ParticleSystem>();

        StartCoroutine(FireworksArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next(){
     //   Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    IEnumerator FireworksArray(){

        yield return new WaitForSeconds(1);

        RightFireworks.Play();
        LeftFireworks.Play();

        yield return new WaitForSeconds(.2f);

        RightFireworks2.Play();
        LeftFireworks2.Play();

        yield return new WaitForSeconds(.2f);

        RightFireworks3.Play();
        LeftFireworks3.Play();

        yield return new WaitForSeconds(.2f);

        RightFireworks4.Play();
        LeftFireworks4.Play();
    }
}
