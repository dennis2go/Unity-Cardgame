using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New CardScript", menuName = "CardScript")]
public class CardScript : ScriptableObject {

	public int id;
	public Sprite artwork;
	public new string name;
	public int mana;
	public int attack;
	public int defense;

}
