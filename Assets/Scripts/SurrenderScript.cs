using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurrenderScript : MonoBehaviour{
    public GameObject surrenderScreen;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void showSurrenderScreen() {
        surrenderScreen.SetActive(true);
    }

    public void surrenderYes() {
        StartCoroutine(surrender());;

    }

    public void surrenderNo() {
        surrenderScreen.SetActive(false);
    }

    IEnumerator surrender() {
        surrenderScreen.SetActive(false);
        yield return new WaitForSeconds(1);
        PlayerHP.currentHP = 0;
    }
}
