using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureCardDatabase : MonoBehaviour {
    public static List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Awake() {
        cards.Add(new Card(0,"None",5,6,7,false,false,"",Resources.Load<Sprite>("Dragon1")));
        cards.Add(new Card(1,"Heliox der Weise",6,6,4,false,false,"",Resources.Load<Sprite>("C7D6E47C-88FB-4AAC-AA73-A659250656B3")));
        cards.Add(new Card(7,"Geist des Waldes",4,2,1,false,true,"erhöht die ATK dieser Karte um die Anzahl an gezahltem Leben  (1-4)",Resources.Load<Sprite>("845C7C8A-8C1A-44F5-90FB-80C2ABB1A5DB")));
        cards.Add(new Card(5,"verlockende Falle",3,1,3,false,true,"kann auch bei gelegten gegnerischen Karten die HP des Gegners direkt angreifen",Resources.Load<Sprite>("B1126B5A-1E3C-48E2-A45C-6AA314749CD3")));
        cards.Add(new Card(9,"Gapple",3,0,0,true,false,"heilt alle eigenen Monsterkarten um 1",Resources.Load<Sprite>("81820141-9924-47F4-86AE-84FCDFAF957D 2")));
        cards.Add(new Card(8,"verbotene Medizin",4,0,0,true,false,"heile die um die Anzahl deines Manas(nach dem beschwören dieser Karte)",Resources.Load<Sprite>("C80E2476-67BA-45F2-8B04-DA4800923A8A 2")));
        
        cards.Add(new Card(4,"Moos-Schamane",3,3,3,false,false,"",Resources.Load<Sprite>("CF5A7901-31FB-4B11-BCF7-4A7BB4D6307F")));
        cards.Add(new Card(6,"Waldwächter",2,1,2,false,true,"fügt der gegenrischen Karte mit meisten ATK 1 Schaden zu",Resources.Load<Sprite>("56256BF6-F907-4489-84C3-D67266F39209")));
        cards.Add(new Card(2,"Pilz-Krieger",1,1,2,false,false,"",Resources.Load<Sprite>("A29C5306-3DDC-4A4F-93DC-6C0761F68F1E")));
        cards.Add(new Card(3,"Schmetterlings-Hybrid",2,2,2,false,false,"",Resources.Load<Sprite>("331988F4-841E-4E49-8B02-2C8A8D32FE0D")));
        cards.Add(new Card(10,"Blumenmädchen",2,0,0,true,false,"heilt deine gespielte Monsterkarte mit den niedrigsten DEF um 1",Resources.Load<Sprite>("743F113B-AFD6-4F13-A09A-0CD151BA03BD")));
    }
}
