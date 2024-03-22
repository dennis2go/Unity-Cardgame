using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnSystem : MonoBehaviour {
    public TextMeshProUGUI manatext;
    public Image manaBar;
    public TextMeshProUGUI enemyManatext;
    public Image enemyManaBar;
    public Button button;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI turnText;


    public static int myCurrentMana;
    private float lerpSpeed; // für manabar
    public static int myMaxMana;
    public static int oponnentCurrentMana;
    public static int oponnentMaxMana;
    public int maximum;
    private string turn;
    public static bool myTurn;
    public static bool oponnentTurn;
    private float totalTime = 160f; // Total time in seconds
    public static float currentTime;
    private bool isTimerRunning = true;
    public static bool startTurn;
    public AudioSource audioSource;
    public AudioClip changeClip;

    void Start() {
        maximum = 10;
        myCurrentMana = 1;
        myMaxMana = 1;
        oponnentCurrentMana = 1;
        oponnentMaxMana = 1;
        StartTimer();
        int x = Random.Range(1,3);
        if(x == 1) {
            myTurn = true;
            turnText.text = "Player";
            startTurn = false;
        }
        else{
            button.interactable = false;
            oponnentTurn = true;
            turnText.text = "Opponent";
            startTurn = true;
        }
    }

    // Update is called once per frame
    void Update() {
        lerpSpeed = 3f * Time.deltaTime;
        Timer();
        manaFiller();
        manatext.text = myCurrentMana + "/" + myMaxMana;
        enemyManatext.text = oponnentCurrentMana + "/" + oponnentMaxMana;
        timerText.text = FormatTime(currentTime);
        if(oponnentTurn && currentTime <= 134f) {
            changeTurnIntern();
        }
    }

    public void Timer() {
        if (isTimerRunning) {
            if (currentTime > 0) {
                currentTime -= Time.deltaTime;
            }
            else {
                changeTurn();
            }
        }
    }

    public void StartTimer() {
        currentTime = totalTime; // Setze die aktuelle Zeit auf die maximale Zeit
        isTimerRunning = true; // Setze den Timer als aktiv
    }


    public void manaAnpassen() {
        // endmyTurn
        if(myTurn) {
            button.interactable = false;
            oponnentMaxMana += 1;
            oponnentCurrentMana = oponnentMaxMana;
            if(oponnentCurrentMana > maximum) {
                oponnentCurrentMana = maximum;
            }
            if(oponnentMaxMana > maximum) {
                oponnentMaxMana = maximum;
            }
            oponnentTurn = true;
            myTurn = false;
            EnemyDeck.draw = true;
            StartTimer();
            turnText.text = "Opponent";
            EnemyDeck.draw = true;
            //startTurn = false;

        }

        else if(oponnentTurn) {
            button.interactable = true;
            myMaxMana += 1;
            myCurrentMana = myMaxMana;
            if(myCurrentMana > maximum) {
                myCurrentMana = maximum;
            }
            if(myMaxMana > maximum) {
                myMaxMana = maximum;
            }
            oponnentTurn = false;
            myTurn = true;
            StartTimer();
            turnText.text = "player";
            PlayerDeck.draw = true;
            //startTurn = true;
        }
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void changeTurn() {
        if(myTurn) {
            audioSource.PlayOneShot(changeClip,0.3f);
            manaAnpassen();
            currentTime = totalTime;
        }
    }

    public void changeTurnIntern() {
            audioSource.PlayOneShot(changeClip,0.3f);
            manaAnpassen();
            currentTime = totalTime;
    }

    public void manaFiller() {
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, (float)myCurrentMana/ maximum, lerpSpeed);
        enemyManaBar.fillAmount = Mathf.Lerp(enemyManaBar.fillAmount, (float)oponnentCurrentMana/ maximum, lerpSpeed);
    }

}
