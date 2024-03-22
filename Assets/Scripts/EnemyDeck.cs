using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : MonoBehaviour {
public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
    public  List<Card> cardsInHand = new List<Card>();

    public List<DisplayCard> playerCards = new List<DisplayCard>();
    public List<DisplayEnemyCard> alreadyAttackedCards = new List<DisplayEnemyCard>();


    public int currentMana;
    public bool[] AiCanSummon;
    public bool drawPhase;
    public bool summonPhase;
    public bool attackPhase;
    public bool endPhase;
    public int[] cardsID;
    public int summonThisID;
    public DisplayEnemyCard displayEnemyCard;
    public int summonID;
    public int howManyCards;

    private int x;
    public static int deckSize;
    private GameObject[] clones;
    public GameObject Hand;
    public GameObject Battlefield;
    public GameObject CardToHand;
    public static bool draw;
    private int counter;
    public static bool temp = true;
    public GameObject playerBattleZone;
    public bool tez = true;
    public bool tez2 = true;
    private bool isAttacking = false;
    public static bool drawCard;
    public AudioSource audioSource;
    public AudioClip attackClip;
    private float waitTime = 2f; // Wartezeit in Sekunden
    private float elapsedTime = 0f;
    private bool attackCounter = true;


    void Start() {
        playerBattleZone = GameObject.Find("BattlefieldPanel");
        //StartCoroutine(WaitFiveSeconds());
        deckSize = 25;
        x= 0;
        int r1 = 0;
        int r2 = 10;
        for(int i=1; i<=5; i++) {
            for(int j=0; j<2; j++) {
                deck[r1] = ShadowCardDatabase.cards[i];
                r1++;
            }
        }       
        for(int i=6; i<=10; i++) {
            for(int j=0; j<3; j++) {
                deck[r2] = ShadowCardDatabase.cards[i];
                r2++;
            }
        }
        Shuffle(deck);
        // for(int i=0; i<deckSize; i++) {
        //     x = Random.Range(1,5);
        //     deck[i] = CardDatabase.cards[x];
        // }
        StartCoroutine(StartGame());
    }

    void Update() {
        staticDeck = deck;
        if(TurnSystem.startTurn == false && draw) {
            StartCoroutine(Draw(1));
            TurnSystem.startTurn = true;
            draw = false;
        }

        if(drawCard) {
            StartCoroutine(Draw(1));
            drawCard = false;
        }

        currentMana = TurnSystem.oponnentCurrentMana;
        // CardsInHand
        if (0 == 0) {
            int j = 0;
            howManyCards = 0;
            foreach (Transform child in Hand.transform) {
                howManyCards++;
            }
            foreach (Transform child in Hand.transform) {
                cardsInHand[j] = child.GetComponent<DisplayEnemyCard>().displaycard[0];
                j++;
            }
            for (int i = 0; i < 25; i++) {
                if (i >= howManyCards) {
                    cardsInHand[i] = ShadowCardDatabase.cards[0];
                }
            }
            j = 0;
        }

        // Summmon
        if(TurnSystem.myTurn == false) {
            for (int i=0;i<25;i++) {
                if(cardsInHand[i].id != 0) {
                    if(currentMana >= cardsInHand[i].mana) {
                        AiCanSummon[i] = true;
                    }
                }
            }
        }
        else {
            for(int i=0;i<25;i++) {
                AiCanSummon[i] = false;
            }
        }
        if(TurnSystem.myTurn == false && summonPhase== false) {
            drawPhase = true;
        }
        if(TurnSystem.myTurn == true) {
            drawPhase = false;
            endPhase = false;
            attackPhase = false;
            summonPhase = false;
        }

        if(drawPhase && summonPhase == false && attackPhase == false) {
            // summonPhase = true;
            StartCoroutine(WaitForSummonPhase());
            drawPhase = false;
            // summonPhase = true;
        }

        if(summonPhase) {
            //drawPhase = false;
            summonID = 0;
            summonThisID = 0;
            int index = 0;
            for (int i=0;i<25;i++) {
                if(AiCanSummon[i] == true) {
                    cardsID[index] = cardsInHand[i].id;
                    index++;
                }
            }
        
            for (int i=0;i<25;i++) {
                if(cardsID[i] != 0) {
                    if(cardsID[i] > summonID) {
                        summonID = cardsID[i];
                    }
                }
            }

            summonThisID = summonID;
            foreach (Transform child in Hand.transform) {
                if(child.GetComponent<DisplayEnemyCard>().id == summonThisID && child.GetComponent<DisplayEnemyCard>().mana <= currentMana) {
                    child.transform.SetParent(Battlefield.transform);
                    TurnSystem.oponnentCurrentMana -= child.GetComponent<DisplayEnemyCard>().mana;
                    break;
                }
            }
            summonPhase = false;
            attackPhase = true;
        }

        if(attackPhase) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTime){
                elapsedTime = 0f;
                StartCoroutine(attackPlayer());
            }
        }
        if(attackPhase && !isAttacking) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTime){
                elapsedTime = 0f;
                isAttacking = true;
                StartCoroutine(attackCards());
            }
            

        }

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

    IEnumerator StartGame(){
        for (int i = 0; i <= 4; i++) {
            yield return new WaitForSeconds(0.8f);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
    }

    IEnumerator Draw(int cards){
        tez = true;
        tez2=true;
        for(int i = 0 ; i < cards ; i++) {
            yield return new WaitForSeconds(1);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
        foreach(Transform childEnemy in Battlefield.transform) {
            childEnemy.GetComponent<DisplayEnemyCard>().cantAttack = false;
        }
    }

    IEnumerator WaitForSummonPhase(){
            drawPhase = false;
            int wait = Random.Range(5,12);
            yield return new WaitForSeconds(6);
            summonPhase = true;
    }

    
    IEnumerator attackCards(){
        yield return new WaitForSeconds(2);
        foreach(Transform childPlayer in playerBattleZone.transform) {
        //tez2 = true;
            foreach(Transform childEnemy in Battlefield.transform) {
                if(childPlayer.GetComponent<DisplayCard>().actualdefense <= childEnemy.GetComponent<DisplayEnemyCard>().attack) {
                    DisplayCard player = childPlayer.GetComponent<DisplayCard>();
                    DisplayEnemyCard enemy = childEnemy.GetComponent<DisplayEnemyCard>();
                    //enemy.aboutToAttack = true;
                    if(enemy.spell == false && enemy.canAttack && player.beInGraveyard == false) {
                        // player.beInGraveyard = true;
                        enemy.canAttack = false;
                        player.attackBorder.SetActive(true);       
                        enemy.summonBorder.SetActive(true);  
                        yield return new WaitForSeconds(3f);
                        schaden(player,enemy);
                        yield return new WaitForSeconds(1);
                        break;
                    }
                }   
            }       
        
        }  
        isAttacking = false;             
    }

    public void schaden(DisplayCard player, DisplayEnemyCard enemy) {
            audioSource.PlayOneShot(attackClip,0.3f);
            enemy.hurted = enemy.hurted + player.actualattack;
            player.hurted = player.hurted + enemy.attack;
            enemy.aboutToAttack = false;
            enemy.cantAttack = true; 
            enemy.summonBorder.SetActive(false);  
    }


    IEnumerator attackPlayer(){ 
        yield return new WaitForSeconds(2);
        playerCards.Clear();
        foreach(Transform childPlayer in playerBattleZone.transform){
            playerCards.Add(childPlayer.GetComponent<DisplayCard>());
        }

        if(playerCards.Count == 0 && tez) {
            foreach(Transform childEnemy in Battlefield.transform) {
                DisplayEnemyCard enemy = childEnemy.GetComponent<DisplayEnemyCard>();
                if(enemy.canAttack && enemy.cantAttack == false && enemy.spell == false) {
                    //yield return new WaitForSeconds(0.3f);
                    enemy.directAttackBorder.SetActive(true);
                    tez = false;
                    yield return new WaitForSeconds(2.2f);
                    audioSource.PlayOneShot(attackClip,0.3f);
                    PlayerHP.currentHP = PlayerHP.currentHP - enemy.attack;
                    enemy.directAttackBorder.SetActive(false);
                }
            }
        }
    }
}