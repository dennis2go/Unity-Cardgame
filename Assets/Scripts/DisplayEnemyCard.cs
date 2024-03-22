using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class DisplayEnemyCard : MonoBehaviour {
    public List<Card> displaycard = new List<Card>();

    private int displayId;

    public int id;
    public string cardname;
    public int mana;
	public int attack;
	public int defense;
    public int actualdefense;
    public bool spell;
    public bool spellmonster;

    public string effect;

	public Sprite artwork;
    public int hurted;

    public TextMeshProUGUI cardnameText;
    public TextMeshProUGUI manatext;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI effectText;

    public Image design;

    public GameObject cardBack;

    public GameObject summonBorder;

    public GameObject directAttackBorder;
    public GameObject spellBorder;

    public GameObject Hand;
    public int numberOfCardsInDeck;

    public GameObject battleZone;
    public GameObject playerBattleZone;

    public bool canAttack;
    public bool cantAttack;

    public bool aboutToAttack;
    // getAttacked
    public bool isTarget;
    public bool thisCardCanBeDestroyed; 
    public bool spellBool;
    private bool isTransitioning = false;
    private Vector3 targetScale;
    private float transitionStartTime;
    private Vector3 originalScale;
    private GameObject currentlyHoveredElement;



    void Start() {
        originalScale = transform.localScale; 
        battleZone = GameObject.Find("EnemyBattlefieldPanel");
        playerBattleZone = GameObject.Find("BattlefieldPanel");
        cardBack.SetActive(true);
        numberOfCardsInDeck = EnemyDeck.deckSize;
        displaycard.Add(ShadowCardDatabase.cards[displayId]);
        //displaycard.Add(CardDatabase.cards[PlayerDeck.deck[]]);
        StartCoroutine(AfterVoidStart());
    }

    
    void Update() {
        //Karte darstellen
        id = displaycard[0].id;
        cardname = displaycard[0].cardname;
        mana = displaycard[0].mana;
        attack = displaycard[0].attack;
        defense = displaycard[0].defense;
        spell = displaycard[0].spell;
        effect = displaycard[0].effect;
        spellmonster = displaycard[0].spellmonster;
        artwork = displaycard[0].artwork;

        actualdefense = defense - hurted;
        cardnameText.text = cardname;
        manatext.text = " " + mana;
        attackText.text = " " + attack;
        defenseText.text = " " + actualdefense;
        effectText.text = " " + effect;
        design.sprite = artwork;
    
        if(spell) {
            attackText.enabled = false;
            defenseText.enabled = false;
        }

        if(this.tag == "Clone") {
            displaycard[0] = EnemyDeck.staticDeck[25 - numberOfCardsInDeck];
            numberOfCardsInDeck -= 1;
            EnemyDeck.deckSize -= 1;
            this.tag = "Untagged";
        }

        if(actualdefense <= 0 && spell == false) {
            Destroy();
        }

        if(this.transform.parent == battleZone.transform) {
            cardBack.SetActive(false);
        }

        if(TurnSystem.oponnentTurn && cantAttack == false) {
            canAttack = true;
        } else {
            canAttack = false;
        }

        if(spell && this.transform.parent == battleZone.transform) {
             StartCoroutine(SpellCard());
        }

        if(spellmonster && this.transform.parent == battleZone.transform) {
            StartCoroutine(SpellMonster());
        }
      
    }

    IEnumerator AfterVoidStart(){
        yield return new WaitForSeconds (1);
        thisCardCanBeDestroyed = true;
        spellBool = true;
    }

    public void BeingTarget(){
        isTarget = true;
    }
    public void DontBeingTarget(){
        isTarget = false;
    }
    public void Destroy() {
        thisCardCanBeDestroyed = true;
        if(thisCardCanBeDestroyed) {
            this.transform.SetParent(null);
            Vector3 newPosition = new Vector3(transform.position.x, -200, transform.position.z);
            transform.position = newPosition;     
            thisCardCanBeDestroyed = false;
            hurted = 0;
        }
    }

    public void Rein() {
        if (this.transform.parent == battleZone.transform && !isTransitioning) {
        targetScale = transform.localScale * 1.36f;
        StartCoroutine(Transition());
        }
    }

    public void Raus() {
        if(this.transform.parent == battleZone.transform) {
            transform.localScale = originalScale;
        }
    }

    private IEnumerator Transition() {
    if(transform.localScale.x <1.1f) {
    isTransitioning = true;
    transitionStartTime = Time.time;
    Vector3 initialScale = transform.localScale;

    while (Time.time - transitionStartTime < 0.03f) {
        float progress = (Time.time - transitionStartTime) / 0.03f;
        transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
        yield return null;
    }

    transform.localScale = targetScale;
    isTransitioning = false;
    }
}


    IEnumerator SpellMonster() {
        switch (cardname){
            case "Polarox":
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(1);
                TurnSystem.oponnentCurrentMana += 1;
                break;
            }
            break;

            case "kristalliner Eismagier":
            if(spellBool) {
                spellBool = false;
                yield return new WaitForSeconds(3);
                int counter = 0;
                foreach(Transform child in playerBattleZone.transform) {
                    counter++;
                }
                this.hurted -= counter;
                break;
            }
            break;

            case "Yasuo des Eises":
            if(spellBool) {       
                spellBool = false;
                yield return new WaitForSeconds(3);
                EnemyDeck.drawCard = true;
                break;
            }
            break;


            //--------------SHADOWW
            case "der stille Tod":
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(3);
                PlayerHP.currentHP -= 1;
                break;
            }
            break;

            case "Illusionist der Schatten":
            if(spellBool) {
                spellBool = false;
                int counter =0;
                foreach(Transform child in playerBattleZone.transform) {
                    counter = counter + 1;
                }
                yield return new WaitForSeconds(0.2f);
                int z = TurnSystem.oponnentCurrentMana;
                this.hurted -= counter;
                break;
            }
            break;

            case "Cerberus":
            List<DisplayCard> tempList = new List<DisplayCard>();
            foreach(Transform child in playerBattleZone.transform) {
                    tempList.Add(child.GetComponent<DisplayCard>());
            }
            tempList.Sort((card1, card2) => card2.actualattack.CompareTo(card1.actualattack));
            if(spellBool) {       
                spellBool = false;
                yield return new WaitForSeconds(2);
                if(tempList.Count > 0) {
                    tempList[0].extraattack -= 1;
                }
                break;
            }
            break;
        }
    }

    IEnumerator SpellCard() {
        switch (cardname){
            case "eisiger Zug":
            if(spellBool) {                
                spellBool = false;
                yield return new WaitForSeconds(3);
                TurnSystem.oponnentMaxMana = TurnSystem.oponnentMaxMana+1;
                yield return new WaitForSeconds(1);
                Destroy();
                break;
            }
            break;

            case "Königsrüstung":
            if(spellBool) {
                spellBool = false;
                yield return new WaitForSeconds(3);
                foreach(Transform child in battleZone.transform) {
                        DisplayEnemyCard xv = child.GetComponent<DisplayEnemyCard>();
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
                yield return new WaitForSeconds(3);
                EnemyHP.currentEnemyHP += 1;
                PlayerHP.currentHP -= 1;
                yield return new WaitForSeconds(1);
                Destroy();
                Debug.Log(cardname);
                break;
            }
            break;
        
            //---------SCHATTEN
            case "Tor der Leere":
            if(spellBool) {                
                spellBool = false;
                spellBorder.SetActive(true);
                yield return new WaitForSeconds(3);
                PlayerHP.currentHP -= 1;
                EnemyHP.currentEnemyHP += 1;
                spellBorder.SetActive(false);
                yield return new WaitForSeconds(1);
                Destroy();
                break;
            }
            break;

            case "geplanter Einschlag":
            if(spellBool) {
                spellBool = false;
                spellBorder.SetActive(true);
                yield return new WaitForSeconds(3);
                EnemyHP.currentEnemyHP -= 3;
                foreach(Transform child in battleZone.transform) {
                    child.GetComponent<DisplayEnemyCard>().hurted -= 1;
                }
                spellBorder.SetActive(false);
                yield return new WaitForSeconds(1);
                Destroy();
                break;
            }
            break;

            case "Leerenportal":
            if(spellBool) {       
                spellBool = false;
                spellBorder.SetActive(true);
                yield return new WaitForSeconds(3);
                TurnSystem.oponnentMaxMana += 1;
                spellBorder.SetActive(false);
                yield return new WaitForSeconds(1);
                Destroy();
                break;
            }
            break;
        }
    }

}


