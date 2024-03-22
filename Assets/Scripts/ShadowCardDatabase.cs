using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCardDatabase : MonoBehaviour {
   public static List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Awake() {
        cards.Add(new Card(0,"None",5,6,7,false,false,"",Resources.Load<Sprite>("Dragon1")));
        cards.Add(new Card(1,"Razork der Unbändige",6,6,4,false,false,"",Resources.Load<Sprite>("F4264F61-AA3F-4796-944C-47946130A225")));
        cards.Add(new Card(7,"Cerberus",3,3,2,false,true,"verringert den ATK-Wert des Gegnermonsters mit dem höhesten Wert um 1",Resources.Load<Sprite>("shadow7")));
        cards.Add(new Card(8,"Tor der Leere",2,0,0,true,false,"heilt dein leben um 1 und schadet dem Gegnerleben um 1",Resources.Load<Sprite>("1213AB67-B406-4BD3-87E9-129616A92E4E")));
        cards.Add(new Card(9,"geplanter Einschlag",4,0,0,true,false,"zahle 3 HP und alle deine Monster erhalten +1 DEF",Resources.Load<Sprite>("shadow9")));
        cards.Add(new Card(10,"Leerenportal",2,0,0,true,false,"erhöht dein maximales Mana um 1",Resources.Load<Sprite>("shadow10")));
        
        cards.Add(new Card(2,"schneller Schatten",3,4,3,false,false,"",Resources.Load<Sprite>("889355D8-0EDD-48D9-9679-844891D5E8A3")));
        cards.Add(new Card(3,"leere Gestalt",1,2,1,false,false,"",Resources.Load<Sprite>("EA0A3321-09BD-46CF-BB06-0CF0FB99660A")));
        cards.Add(new Card(4,"Schattengoblin",1,2,2,false,false,"",Resources.Load<Sprite>("shadow4")));
        cards.Add(new Card(5,"der stille Tod",2,3,2,false,true,"füge dem Gegenspieler direkt 1 Schaden zu",Resources.Load<Sprite>("FF32D8C6-A7A1-4356-A8E1-935365C1AA84")));
        cards.Add(new Card(6,"Illusionist der Schatten",2,1,1,false,true,"auf seine 1 DEF werden pro Gegnerkarte im Spielfeld + 1DEF heraufgerechnet",Resources.Load<Sprite>("533AB4C5-01CB-40EF-810A-37B878901080")));
    }
}
