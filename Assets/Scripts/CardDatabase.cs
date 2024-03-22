using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    public static List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Awake() {
        cards.Add(new Card(0,"None",5,6,7,false,false,"",Resources.Load<Sprite>("Dragon1")));
        cards.Add(new Card(1,"Insigniar der Älteste",6,6,4,false,false,"",Resources.Load<Sprite>("6EF064D2-CF82-40BE-8B27-BDA7264CCC88")));
        cards.Add(new Card(8,"heiße Angelegenheit",4,0,0,true,false,"zahle 3 Lifepoints und zerstöre die gegnerische Karte mit den höhesten Atk-Werten",Resources.Load<Sprite>("5D28FB90-E091-4C11-A452-E715638D75CE")));
        cards.Add(new Card(4,"gehärteter Schildner",3,2,5,false,false,"",Resources.Load<Sprite>("799C043A-A1BD-471D-AF48-B925FF5B6A51")));
        cards.Add(new Card(6,"verbrannte Sense",4,3,2,false,true,"zerstört 1 zufälliges Gegner- und 1 zufälliges Spielermonster",Resources.Load<Sprite>("3BA324EA-5E71-4A6C-86CB-3B72A16CCFCF 2")));
        cards.Add(new Card(5,"Magmapolyp",2,1,2,false,true,"füge dem Gegenspieler direkt 1 Schaden zu",Resources.Load<Sprite>("507FC268-7564-48AB-9092-D7F3B7F2D5FD")));
        
        cards.Add(new Card(2,"Feuerscherge",4,4,4,false,false,"",Resources.Load<Sprite>("51F72820-7701-453A-BE73-8CDA96CD9802 2")));
        cards.Add(new Card(3,"kleiner Go",1,1,1,false,false,"",Resources.Load<Sprite>("2F56F03E-9E0A-4AF0-9252-4198E365B7E9 2")));
        cards.Add(new Card(7,"Magmahund",2,2,1,false,true,"ziehe eine Karte",Resources.Load<Sprite>("6A4FBF9A-E6EB-4398-B3F4-6D9F4BE42204 2")));
        cards.Add(new Card(9,"Feuerhand",2,0,0,true,false,"zahle 1 oder 2 Lebenspunkte und ziehe somit 1 oder 2 Karten",Resources.Load<Sprite>("FECE9CEF-5BDE-404F-8F3D-EB5313B1AC15 2")));
        cards.Add(new Card(10,"Magmaherz",3,0,0,true,false,"heilt deine Lebenspunkte um 2",Resources.Load<Sprite>("E3D34DFD-B9FA-4052-820A-F2674F7D76C5")));
    }

}
