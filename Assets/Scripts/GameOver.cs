using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public AudioSource audioSource; 
    public AudioClip looseSound;
    public AudioClip endSound;
    void Start() {

    }

    void Update() {
        if (PlayerHP.currentHP == 0) {
            defeat();
        }
        else if (EnemyHP.currentEnemyHP == 0) {
            victory();
        }
    }

    public void victory() {
        audioSource.Play();
        // audioSource.PlayOneShot(endSound,1f);
        victoryScreen.SetActive(true);
        StartCoroutine(ReturnToMenu());
    }

    public void defeat() {
        audioSource.Play();
        // audioSource.PlayOneShot(endSound,1f);
        defeatScreen.SetActive(true);
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu() {   
        yield return new WaitForSeconds(2.7f);
        SceneManager.LoadScene("Menu");
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
    }
}
