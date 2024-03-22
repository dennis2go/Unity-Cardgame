using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayCard : MonoBehaviour {
    public List<Card> displaycard = new List<Card>();
    private int displayId;
    private int counterForEnemyAttack;
    public int id;
    public string cardname;
    public int mana;
	public int attack;
	public int defense;
    public bool spell;
    public bool spellmonster;
    public string effect;
    public int actualdefense;
     public int actualattack;
	public Sprite artwork;
    public int hurted;
    public int extraattack;
    public TextMeshProUGUI cardnameText;
    public TextMeshProUGUI manatext;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI effectText;

    public Image design;

    public GameObject cardBack;

    public GameObject summonBorder;
    public GameObject attackBorder;

    public GameObject feuerhand;
    public GameObject Waldgeist;

    public GameObject Hand;
    public int numberOfCardsInDeck;

    // nur Karte wenn genug Mana
    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    // Attack Enemy Health
    public GameObject target;
    public GameObject enemy;
    public bool summoningSickness;
    private bool cantAttack;
    private bool canAttack;
    private static bool staticTargeting;
    private static bool staticTargetingEnemy;
    private bool targeting;
    private bool targetingEnemy;
    public bool onlyThisCardAttack;
    public GameObject EnemyZone;
    public GameObject PlayerZone;

    public DisplayEnemyCard displayEnemyCard;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    private bool isTransitioning = false;
    private float transitionStartTime;

    // destroy Card
    public bool canBeDestroyed;
    public bool beInGraveyard;
    private bool spellBool;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private GameObject currentlyHoveredElement;
    public Transform theParent;
    private int waldwächterCounter;
    public AudioSource audioSource;
    public AudioClip attackClip;

    void Start() {
        originalScale = transform.localScale; 
        cardBack.SetActive(false);
        summonBorder.SetActive(false);
        attackBorder.SetActive(false);
        feuerhand.SetActive(false);
        numberOfCardsInDeck = PlayerDeck.deckSize;
        if(PlayerDeck.pickedDeck == "magma") {
            displaycard.Add(CardDatabase.cards[displayId]);
        }
        if(PlayerDeck.pickedDeck == "eis") {
            displaycard.Add(IceCardDatabase.cards[displayId]);
        }
        if(PlayerDeck.pickedDeck == "natur") {
            displaycard.Add(NatureCardDatabase.cards[displayId]);
        }
        //displaycard.Add(CardDatabase.cards[PlayerDeck.deck[]]);
        summoningSickness = true;
        canBeSummon = false;
        summoned = false;
        canAttack = false;
        enemy = GameObject.Find("EnemyHP");
        targeting = false;
        targetingEnemy = false;

        EnemyZone = GameObject.Find("EnemyBattlefieldPanel");
        PlayerZone = GameObject.Find("BattlefieldPanel");

    }

    // Update is called once per frame
    void Update() {
        id = displaycard[0].id;
        cardname = displaycard[0].cardname;
        mana = displaycard[0].mana;
        attack = displaycard[0].attack;
        defense = displaycard[0].defense;
        spell = displaycard[0].spell;
        artwork = displaycard[0].artwork;
        effect = displaycard[0].effect;
        spellmonster = displaycard[0].spellmonster;
        //Karte darstellen
        actualdefense = defense-hurted;
        actualattack = attack + extraattack;
        cardnameText.text = cardname;
        manatext.text = " " + mana;
        attackText.text = " " + actualattack;
        defenseText.text = " " + actualdefense;
        effectText.text = " " + effect;
        design.sprite = artwork;
    
        if(spell) {
            attackText.enabled = false;
            defenseText.enabled = false;
            canAttack = false;
            cantAttack = true;
        }
        if(this.tag == "Clone") {
            displaycard[0] = PlayerDeck.staticDeck[25 - numberOfCardsInDeck];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            this.tag = "Untagged";
        }
        // Nur wenn genug Mana dann Karte legen
        if(TurnSystem.myCurrentMana >= mana && summoned == false && beInGraveyard == false && TurnSystem.myTurn) {
            canBeSummon = true;
            summonBorder.SetActive(true);
        } else{
            canBeSummon = false;
        }
        if(canBeSummon == false) {
            gameObject.GetComponent<Draggable>().enabled = false;
            summonBorder.SetActive(false);
        } else{
            gameObject.GetComponent<Draggable>().enabled = true;
        }
        battleZone = GameObject.Find("BattlefieldPanel");
        if(summoned == false && this.transform.parent == battleZone.transform) {
            Summon();
        }
        
        // Gegner HP attackieren
        if(TurnSystem.myTurn == false && summoned == true) {
            cantAttack = false;
        }
        if(TurnSystem.myTurn && cantAttack == false) {
            canAttack = true;
        } else {
            canAttack = false;
        }
        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;
        if(targetingEnemy) {
            target = enemy;
        } else {
            target = null;
        }
        if(targeting && onlyThisCardAttack) {
            Attack();
        }
        if(actualdefense <= 0 && spell == false) {
            Destroy();
        }

        if(spellmonster && summoned) {
            StartCoroutine(SpellMonster());
        }

        if(spell && summoned) {
            StartCoroutine(SpellCard());
        }

        if(TurnSystem.oponnentTurn) {
            Arrow.hide = true;
        }

        if(transform.position.y <= 0) {
            Vector3 newPosition = transform.position + Vector3.up * 160f; // Erstellen einer Kopie der aktuellen Position
            transform.position = newPosition; 
        }
    }

    public void Summon() {
        TurnSystem.myCurrentMana -= mana;
        summoned = true;
        spellBool = true;
    }

    public void EndArrow() {
        Arrow.hide = true;
    }

     public void Attack() {
        if(canAttack && summoned) {
            if(target != null) {
                if(target == enemy) {
                    foreach(Transform child in EnemyZone.transform) {
                        counterForEnemyAttack++;
                    }
                    if(counterForEnemyAttack == 0 || cardname == "verlockende Falle") {
                    audioSource.PlayOneShot(attackClip,0.3f);
                    EnemyHP.currentEnemyHP -= actualattack;
                    targeting = false;
                    cantAttack = true;
                    }
                    Arrow.hide = true;
                }
                
            }
            else{
                foreach(Transform child in EnemyZone.transform) {
                    if(child.GetComponent<DisplayEnemyCard>().isTarget == true) {
                        child.GetComponent<DisplayEnemyCard>().hurted =child.GetComponent<DisplayEnemyCard>().hurted + actualattack;
                        audioSource.PlayOneShot(attackClip,0.3f);
                        hurted = hurted +  child.GetComponent<DisplayEnemyCard>().attack;
                        cantAttack = true;
                        Arrow.hide = true;
                    }
                }
            }
        }
    }
    // attack
    public void UntargetEnemy() {
        staticTargetingEnemy = false;
    }

    public void TargetEnemy() {
        staticTargetingEnemy = true;
    }

    public void StartAttack() {
        staticTargeting = true;
        if(canAttack && summoned) {
            Arrow.show = true;
            Arrow.startPoint = transform.position;
        }
    }

    public void StopAttack() {
        staticTargeting = false;
    }

    public void OneCardAttack() {
        onlyThisCardAttack = true;
    }

    public void OneCardAttackStop() {
        onlyThisCardAttack = false;
    }

    public void Rein() {
        if (!summoned && !isTransitioning && PlayerDeck.canHover && TurnSystem.myTurn && PlayerDeck.newHover) {
        theParent = this.transform.parent;
        targetScale = transform.localScale * 1.3f;
        targetPosition = transform.position + Vector3.up * 160f;
        transform.localScale = targetScale;
        transform.position = targetPosition;
        }
    }

    public void Raus() {
        if(!summoned && PlayerDeck.canHover && TurnSystem.myTurn && PlayerDeck.newHover) {
            transform.localScale = originalScale;
            transform.position = transform.position + Vector3.down * 160f;
        }
    }

    public void DragStopper1() {
        PlayerDeck.canHover = false;
    }

    public void DragStopper2() {
        transform.localScale = originalScale;
        PlayerDeck.canHover = true;
    }
    
    public void Destroy() {
        canBeDestroyed = true;
        if(canBeDestroyed) {
            this.transform.SetParent(null);
            Vector3 newPosition = new Vector3(transform.position.x, -200, transform.position.z);
            transform.position = newPosition;     
            canBeDestroyed = false;
            summoned = false;
            beInGraveyard = true;
            hurted = 0;
        }
    }

    IEnumerator SpellMonster() {
        switch (cardname){
            //MAGMA--DECK
            case "Magmapolyp":
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(1);
                EnemyHP.currentEnemyHP -= 1;
                break;
            }
            break;

            case "verbrannte Sense":
            List<DisplayEnemyCard> tempListEnemy = new List<DisplayEnemyCard>();
            List<DisplayCard> tempListPlayer = new List<DisplayCard>();
            foreach(Transform childEnemy in EnemyZone.transform) {
                tempListEnemy.Add(childEnemy.GetComponent<DisplayEnemyCard>());
            }
            foreach(Transform childPlayer in PlayerZone.transform) {
                tempListPlayer.Add(childPlayer.GetComponent<DisplayCard>());
            }
            int x1 = Random.Range(0,tempListEnemy.Count);
            int x2 = Random.Range(0,tempListPlayer.Count);
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(1);
                tempListEnemy[x1].defense = 0;
                tempListEnemy[x1].hurted = 10;
                tempListPlayer[x2].defense = 0;
                tempListPlayer[x2].hurted = 10;
                break;
            }
            break;

            case "Magmahund":
            if(spellBool) {       
                spellBool = false;
                yield return new WaitForSeconds(0.5f);
                PlayerDeck.drawCard = true;
                break;
            }
            break;


            //--------EIS--DECK
            case "Polarox":
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(1);
                TurnSystem.myCurrentMana += 1;
                break;
            }
            break;

            case "kristalliner Eismagier":
            if(spellBool) {
                spellBool = false;
                yield return new WaitForSeconds(1);
                int counter = 0;
                foreach(Transform child in EnemyZone.transform) {
                    counter++;
                }
                this.hurted -= counter;
                break;
            }
            break;

            case "Yasuo des Eises":
            if(spellBool) {       
                spellBool = false;
                yield return new WaitForSeconds(1);
                PlayerDeck.drawCard = true;
                break;
            }
            break;

             //--------NATUR--DECK
            case "verlockende Falle":
            if(spellBool) {       
                spellBool = false;
                break;
            }
            break;

            case "Waldwächter":
            List<DisplayEnemyCard> tempList = new List<DisplayEnemyCard>();
            foreach(Transform child in EnemyZone.transform) {
                    tempList.Add(child.GetComponent<DisplayEnemyCard>());
            }
            tempList.Sort((card1, card2) => card2.attack.CompareTo(card1.attack));
            if(spellBool) {       
                spellBool = false;
                yield return new WaitForSeconds(1.4f);
                if(tempList.Count > 0) {
                    tempList[0].hurted += 1;
                }
                break;
            }
            break;

            case "Geist des Waldes":
            if(spellBool) {               
                spellBool = false;
                yield return new WaitForSeconds(1);
                Waldgeist.SetActive(true);
                break;
            }
            break;
        }
    }

    IEnumerator SpellCard() {
        //MAGMA--DECK
        switch (cardname){
            case "heiße Angelegenheit":
                List<DisplayEnemyCard> tempList = new List<DisplayEnemyCard>();
                foreach(Transform child in EnemyZone.transform) {
                    tempList.Add(child.GetComponent<DisplayEnemyCard>());
                }
                tempList.Sort((card1, card2) => card2.attack.CompareTo(card1.attack));
                if(spellBool) {                
                    spellBool = false;
                    yield return new WaitForSeconds(2);
                    PlayerHP.currentHP -= 3;
                    if(tempList.Count > 0) {
                        tempList[0].defense = 0;
                        tempList[0].hurted = 10;
                    }
                    Destroy();
                break;
                }   
            break;

            case "Feuerhand":
                if(spellBool) {
                    spellBool = false;
                    yield return new WaitForSeconds(1);
                    feuerhand.SetActive(true);
                    break;
                }
            break;

            case "Magmaherz":
                if(spellBool) {
                    spellBool = false;
                    yield return new WaitForSeconds(1);
                    PlayerHP.currentHP += 2;
                    yield return new WaitForSeconds(1);
                    Destroy();
                    Debug.Log(cardname);
                    break;
                }
            break;
        //EIS --DECK------
            case "eisiger Zug":
                if(spellBool) {                
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    TurnSystem.myMaxMana = TurnSystem.myMaxMana+1;
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;

            case "Königsrüstung":
                if(spellBool) {
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    foreach(Transform child in PlayerZone.transform) {
                        DisplayCard xv = child.GetComponent<DisplayCard>();
                        if(!xv.spell) {
                            xv.hurted -= 1;
                        }
                    }
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;

            case "frostiges Siegel":
                if(spellBool) {
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    PlayerHP.currentHP += 1;
                    EnemyHP.currentEnemyHP -= 1;
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;

            //NATUR --DECK------
            case "verbotene Medizin":
                if(spellBool) {                
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    int temp = TurnSystem.myCurrentMana;
                    PlayerHP.currentHP += temp;
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;

            case "Gapple":
                if(spellBool) {                
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    foreach(Transform child in PlayerZone.transform) {
                        DisplayCard xv = child.GetComponent<DisplayCard>();
                        if(!xv.spell) {
                            xv.hurted -= 1;
                        }
                    }
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;

            case "Blumenmädchen":
                List<DisplayCard> tempList2 = new List<DisplayCard>();
                foreach(Transform child in PlayerZone.transform) {
                    if(!child.GetComponent<DisplayCard>().spell) {
                    tempList2.Add(child.GetComponent<DisplayCard>());

                    }
                }               
                tempList2.Sort((card1, card2) => card1.actualdefense.CompareTo(card2.actualdefense));                
                if(spellBool) {                
                    spellBool = false;
                    yield return new WaitForSeconds(1.4f);
                    if(tempList2.Count > 0) {
                        tempList2[0].hurted = hurted - 1;
                    }
                    yield return new WaitForSeconds(1);
                    Destroy();
                    break;
                }
            break;
        }

    }

    public void feuerhand1Ziehen() {
        feuerhand.SetActive(false);
        PlayerHP.currentHP -= 1;
        PlayerDeck.drawCard = true;
        Destroy();
    }

    public void feuerhand2Ziehen() {
        PlayerDeck.drawCard2 = true;
        PlayerHP.currentHP -= 2;
        feuerhand.SetActive(false);
        Destroy();
    }

    public void geistDesWaldes1() {
        Arrow.hide = true;
        this.extraattack = 1;
        PlayerHP.currentHP -= 1;
        Waldgeist.SetActive(false);
    }

    public void geistDesWaldes2() {
        Arrow.hide = true;
        this.extraattack = 2;
        PlayerHP.currentHP -= 2;
        Waldgeist.SetActive(false);
    }

    public void geistDesWaldes3() {
        Arrow.hide = true;
        PlayerHP.currentHP -= 3;
        this.extraattack = 3;
        Waldgeist.SetActive(false);
    }

    public void geistDesWaldes4() {
        Arrow.hide = true;
        PlayerHP.currentHP -= 4;
        this.extraattack = 4;
        Waldgeist.SetActive(false);
    }
}
