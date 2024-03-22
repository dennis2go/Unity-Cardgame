using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card {
    public int id;
	public string cardname;
	public int mana;
	public int attack;
	public int defense;
	public Sprite artwork;

    public bool showBack;
    public bool spell;
    public string effect;
    public bool spellmonster;

    public Card(int id, string cardname, int mana, int attack, int defense, bool spell, bool spellmonster, string effect, Sprite artwork) {
        this.id = id;
        this.cardname = cardname;
        this.mana = mana;
        this.attack = attack;
        this.defense = defense;
        this.spell = spell;
        this.spellmonster = spellmonster;
        this.effect = effect;
        this.artwork = artwork;
    }
}
