using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerDeck : MonoBehaviour {
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
    private int x;
    public static int deckSize;
    public GameObject[] clones;
    public GameObject Hand;
    public GameObject CardToHand;
    public static bool draw;
    public AudioSource audioSource;
    public AudioSource backgroundAudioSource;

    public AudioClip drawClip;
    public AudioClip backgroundClip;
    public AudioClip looseSound;

    public static bool drawCard;
    public static bool drawCard2;
    private bool beginningDraw = true;
    public static string pickedDeck = "magma";
    public Image design;
    public static bool canHover = true;
    public static bool newHover;
    public static bool newHoverRaus;


    void Start() {
        switch(pickedDeck) {
                case "magma":
                design.sprite = Resources.Load<Sprite>("fire_leader");
                break;
                case "eis":
                design.sprite = Resources.Load<Sprite>("ice_leader");
                break;
                case "natur":
                design.sprite = Resources.Load<Sprite>("waldnation_pic");
                break;
            }
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = 0.35f;
        backgroundAudioSource.Play();
        deckSize = 25;
        x= 0;
        int r1 = 0;
        int r2 = 10;
        for(int i=1; i<=5; i++) {
            for(int j=0; j<2; j++) {
                switch(pickedDeck) {
                    case "magma":
                        deck[r1] = CardDatabase.cards[i];
                        r1++;
                    break;
                    case "eis":
                        deck[r1] = IceCardDatabase.cards[i];
                        r1++;
                    break;
                    case "natur":
                        deck[r1] = NatureCardDatabase.cards[i];
                        r1++;
                    break;
                }
            }
        }       
        for(int i=6; i<=10; i++) {
            for(int j=0; j<3; j++) {
                switch(pickedDeck) {
                    case "magma":
                        deck[r2] = CardDatabase.cards[i];
                        r2++;
                    break;
                    case "eis":
                        deck[r2] = IceCardDatabase.cards[i];
                        r2++;
                    break;
                    case "natur":
                        deck[r2] = NatureCardDatabase.cards[i];
                        r2++;
                    break;
                }
            }
        }
        Shuffle(deck);
        StartCoroutine(StartGame());
    }

    void Update() {
        staticDeck = deck;
        if(TurnSystem.startTurn && draw) {
            StartCoroutine(Draw(1));
            TurnSystem.startTurn = false;
            draw = false;
        }

        if(deckSize <= 0) {
            PlayerHP.currentHP = 0;
        }

        if(PlayerHP.currentHP == 0 || EnemyHP.currentEnemyHP == 0) {
            backgroundAudioSource.Stop();
        }

        if(drawCard) {
            StartCoroutine(Draw(1));
            drawCard = false;
        }

        if(drawCard2) {
            StartCoroutine(Draw(2));
            drawCard2 = false;
        }

        if(TurnSystem.oponnentTurn) {
            newHover = false;
        }
    }

    IEnumerator StartGame(){
        for (int i = 0; i <= 4; i++) {
            audioSource.PlayOneShot(drawClip,1f);
            yield return new WaitForSeconds(0.8f);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
        newHover = true;
        StartCoroutine(ja());
    }

    IEnumerator ja(){
        yield return new WaitForSeconds(1);
        newHoverRaus = true;
    }

    IEnumerator Draw(int cards){
        for(int i = 0 ; i < cards ; i++) {
            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(drawClip,1f);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
        newHover = true;
    }

    public void Shuffle<T>(List<T> list) {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
