using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour{
    public string play;
    public string settings;
    public string menu;
    public string anleitung;
    public AudioSource audioSource;
    public AudioClip drawClip;
    public AudioSource backgroundAudioSource;
    public AudioClip backgroundClip;

    void Start(){
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = 0.1f;
        backgroundAudioSource.Play();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void loadPlay(){
        SceneManager.LoadScene(play);
        audioSource.PlayOneShot(drawClip,1f);
    }

    public void loadSettings(){
        // SceneManager.LoadScene(settings);
        audioSource.PlayOneShot(drawClip,1f);
    }

    public void loadAnleitung(){
        SceneManager.LoadScene(anleitung);
        audioSource.PlayOneShot(drawClip,1f);
    }

    public void loadDecks(){
        SceneManager.LoadScene("Decks");
        audioSource.PlayOneShot(drawClip,1f);
    }

    public void returnToMenu(){
        SceneManager.LoadScene(menu);
        audioSource.PlayOneShot(drawClip,1f);
    }

    public void exitGame(){
        Application.Quit();
        audioSource.PlayOneShot(drawClip,1f);
    }
}
