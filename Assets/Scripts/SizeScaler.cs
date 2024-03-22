using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SizeScaler : MonoBehaviour {
    // Start is called before the first frame update
    public float scaleFactor = 0.4f; // Faktor, um den das Objekt vergrößert werden soll

    private Vector3 originalScale; // Ursprüngliche Skalierung des Objekts
    private Vector3 originalPosition;
    private Transform parent;
    public GameObject hand;

    private void Start() {
        originalScale = transform.localScale; 
        originalPosition = transform.position;
        parent = transform.parent;
        hand = GameObject.Find("PlayerHandPanel");
    }

    private void Update() {

    }

    public void Rein() {
        Debug.Log("1"+hand);
        Debug.Log("2"+transform.parent);
        if(transform.parent == hand) {
            transform.localScale = transform.localScale * 1.8f;
            this.transform.Translate(Vector3.up * 160f, Space.World);
        }
        //transform.position = new Vector3(originalPosition.x, originalPosition.y +4, originalPosition.z);
    }

    public void Raus() {
        if( transform.parent == hand) {
            transform.localScale = originalScale;
            this.transform.Translate(Vector3.up * -160f, Space.World);
        }
        // Debug.Log(parent);
    }

}
