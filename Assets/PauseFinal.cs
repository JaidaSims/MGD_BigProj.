using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFinal : MonoBehaviour
{

    public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject MainControls;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Play(){
        //Unpauses game
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            MainControls.SetActive(true);
           
            // Load();
    
            gameIsPaused = !gameIsPaused;
    }

    public void Pause(){
        if(gameIsPaused) {
            //Pauses game because it is not paused
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            MainControls.SetActive(false);
            // Save();
        }
        //pauseMenu.SetActive(!gameIsPaused);
            gameIsPaused = !gameIsPaused;
    }

    //Should be on player for player health?
    //  void Save(){
    //      PlayerPrefs.SetInt("Health", health);
    //      PlayerPrefs.SetInt("Score", score);
    //  }

    //  void Load(){
    //      health = PlayerPrefs.GetInt("Health");
    //      score = PlayerPrefs.GetInt("Score");
    //  }


}
