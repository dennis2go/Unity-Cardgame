using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour {
    
    public GameObject Hand;
    public GameObject HandCard;

    void Start() {
        Hand = GameObject.Find("PlayerHandPanel");
        HandCard.transform.SetParent(Hand.transform);
        HandCard.transform.localScale = Vector3.one;
        HandCard.transform.position = new Vector3(transform.position.x,transform.position.y, -48);
        
    }

    // Update is called once per frame
    void Update() {
    }
}
