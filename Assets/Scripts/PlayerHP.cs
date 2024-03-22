using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour{
    public Image healthBar;
    public static float maxHP;
    public static float currentHP;
    private float lerpSpeed; 
    public TextMeshProUGUI healthText;

    public static bool lost;
    public static bool win;
    void Start(){
        currentHP = 15f;
        maxHP = 15f;
        lerpSpeed = 3f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update(){
        if(currentHP < 0) {
            currentHP = 0;
        }
        if(currentHP == 0) {
            lost = true;
        }
        healthFiller();
    }

      private void healthFiller() {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHP/ maxHP, lerpSpeed);
        healthText.text = currentHP + "HP";
    }
}
