using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWW_Music : MonoBehaviour
{
    public string url = "https://jaidasims.github.io/JaidaSims/Music/Dream%20Raid%20Part%20I.mp3";

    public AudioClip webClip;
    AudioSource aud;

    IEnumerator GetAudioFromWeb(){
        using(WWW www = new WWW(url)){
            while(www.progress != 1){
                Debug.Log(www.progress);
          //      progressBar.value = www.progress;
                yield return new WaitForEndOfFrame();
            }


            webClip = www.GetAudioClip();
            aud.clip = webClip;
            aud.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        StartCoroutine(GetAudioFromWeb());  
    }

}
