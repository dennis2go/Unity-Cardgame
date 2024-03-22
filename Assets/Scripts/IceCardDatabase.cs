using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCardDatabase : MonoBehaviour {
    public static List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Awake() {
        cards.Add(new Card(0,"None",5,6,7,false,false,"",Resources.Load<Sprite>("Dragon1")));
        cards.Add(new Card(1,"Frirock der Bedachte",6,6,4,false,false,"",Resources.Load<Sprite>("2994DA44-CC31-49E7-8061-B58F5FD90E9B")));
        cards.Add(new Card(8,"eisiger Zug",3,0,0,true,false,"erhöht dein maximales Mana um 1",Resources.Load<Sprite>("56A00EC4-56B3-4E91-8988-54B8CE5FA2F3")));
        cards.Add(new Card(5,"Polarox",2,1,3,false,true,"du erhälst direkt 1 Mana",Resources.Load<Sprite>("C7289C67-674C-4666-A5AB-73C01CB9B1F9")));
        cards.Add(new Card(9,"Königsrüstung",4,0,0,true,false,"erhöht die Verteidigungskraft aller Einehiten um 1",Resources.Load<Sprite>("DA7F0C40-12A3-4AE7-BF5C-F6669215F1F9")));
        cards.Add(new Card(6,"kristalliner Eismagier",4,3,1,false,true,"erhält pro Monsterkarte des Gegners +1 Verteidigungswert",Resources.Load<Sprite>("509A6043-0F7D-461D-BC7A-BC20AB3F12B9")));
        
        cards.Add(new Card(2,"Höhlengeist",1,1,2,false,false,"",Resources.Load<Sprite>("7696C6A9-8DD5-424B-AC3C-C7E2986ECC27")));
        cards.Add(new Card(3,"Schild des Nordens",3,1,4,false,false,"",Resources.Load<Sprite>("31F975FF-595F-4B4C-920D-5A04899882FC")));
        cards.Add(new Card(4,"Nebulosaurus",2,2,2,false,false,"",Resources.Load<Sprite>("CD1A50F1-D24C-43C5-B09F-1521740383F3")));
        cards.Add(new Card(7,"Yasuo des Eises",3,2,2,false,true,"zieht eine Karte",Resources.Load<Sprite>("972FD335-0B4F-4C7D-B5DD-4D2D79397A41 2")));
        cards.Add(new Card(10,"frostiges Siegel",3,0,0,true,false,"fügt dem Gegenspieler direkt 1 Schaden zu und heilt dich selbst um 1",Resources.Load<Sprite>("1DE9FCFD-C89D-48D5-8E55-EDD2CDD27358")));
    }
}
