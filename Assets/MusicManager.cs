using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

         if (objs.Length > 1){
             Destroy(this.gameObject);
         }

        DontDestroyOnLoad(this.gameObject);
    }

    public int mainMenuIndex = 0;
    public int world1Index = 1;
    public int world2Index = 4; //Different theme/music for next set of level
    public int world3Index = 7;
    
    public List<AudioClip> songs = new List<AudioClip>();
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        aud.clip = songs[0];
        aud.Play();
    }

    public IEnumerator StartNextLevel(){
        yield return new WaitForSeconds(.5f);

        int currentIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(currentIndex == mainMenuIndex){
            aud.clip = songs[0];
            aud.Play();
        } else if(currentIndex == world1Index){
            aud.clip = songs[1];
            aud.Play();
        } else if (currentIndex == world2Index){
            aud.clip = songs[2];
            aud.Play();
        } else if (currentIndex == world3Index){
            aud.clip = songs[3];
            aud.Play();
        }
    }
}
