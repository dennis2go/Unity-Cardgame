using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anleitung : MonoBehaviour {
    public GameObject Nummer1;
    public GameObject Nummer2;
    public GameObject Nummer3;
    public GameObject Nummer4;
    public GameObject Nummer5;
    public GameObject Nummer6;
    public AudioSource backgroundAudioSource;
    public AudioClip backgroundClip;

    void Start(){
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = 0.1f;
        backgroundAudioSource.Play();
    }

    public void von1zu2() {
        Nummer1.SetActive(false); 
        Nummer2.SetActive(true); 
    }

    public void von2zu3() {
        Nummer2.SetActive(false); 
        Nummer3.SetActive(true); 
    }

    public void von3zu4() {
        Nummer3.SetActive(false); 
        Nummer4.SetActive(true); 
    }

    public void von4zu5() {
        Nummer4.SetActive(false); 
        Nummer5.SetActive(true);   
    }

    public void von5zu6() {
        Nummer5.SetActive(false); 
        Nummer6.SetActive(true);   
    }

     public void von6zu5() {
        Nummer6.SetActive(false); 
        Nummer5.SetActive(true); 
    }
    public void von5zu4() {
        Nummer5.SetActive(false); 
        Nummer4.SetActive(true); 
    }

     public void von4zu3() {
        Nummer4.SetActive(false); 
        Nummer3.SetActive(true); 
    }

    public void von3zu2() {
        Nummer3.SetActive(false); 
        Nummer2.SetActive(true); 
    }

    public void von2zu1() {
        Nummer2.SetActive(false); 
        Nummer1.SetActive(true); 
    }

    public void returnToMenu(){
        SceneManager.LoadScene("Menu");
    }
}

