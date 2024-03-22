using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeckChooser : MonoBehaviour{
    public Image design;
    public Sprite artwork;
    public TextMeshProUGUI description;
    public GameObject bild;
    public AudioSource audioSource;
    public AudioClip drawClip;
    public AudioSource backgroundAudioSource;
    public AudioClip backgroundClip;
    private bool counter;

    // Start is called before the first frame update
    void Start(){
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = 0.2f;
        backgroundAudioSource.Play();
    }

    // Update is called once per frame
    void Update(){

    }

    public void startGame(){
        if(counter) {
            audioSource.PlayOneShot(drawClip,1f);
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void magma(){
        counter = true;
        audioSource.PlayOneShot(drawClip,1f);
        description.text = "Das Magmavolk setzt auf eine ausgeglichene Mischung von starken Monstern und Zaubern und ist somit gegen alle Gefahren breit gefächert.";
        bild.SetActive(true);
        artwork = Resources.Load<Sprite>("MagmaDeck");
        design.sprite = artwork;
        PlayerDeck.pickedDeck = "magma";
    }

    public void ice(){
        counter = true;
        audioSource.PlayOneShot(drawClip,1f);
        description.text = "Das Eisvolk legt ihren Wert darauf sich mit hohen Defensivwerten zu schützen und dem Gegner, somit kein Vorbeikommen zu ermöglichen.";
        bild.SetActive(true);
        artwork = Resources.Load<Sprite>("EisDeck");
        design.sprite = artwork;
        PlayerDeck.pickedDeck = "eis";
    }

    public void nature(){
        counter = true;
        audioSource.PlayOneShot(drawClip,1f);
        description.text = "Das Naturvolk setzt vor allem auf Zauber und Monster mit Heilkräften, um erlittenen Schaden gleichgültig zu machen.";       
        bild.SetActive(true);
        artwork = Resources.Load<Sprite>("NaturDeck");
        design.sprite = artwork;
        PlayerDeck.pickedDeck = "natur";
    }

    public void shadow(){
        // counter = true;
        // audioSource.PlayOneShot(drawClip,1f);
        // disableBorders();
        // summonBorderShadow.SetActive(true);
        // description.text = "Das Schattenvolk ";
        // bild.SetActive(true);
        // artwork = Resources.Load<Sprite>("SchattenDeck");
        // design.sprite = artwork;
        // PlayerDeck.pickedDeck = "schatten";
    }

}
