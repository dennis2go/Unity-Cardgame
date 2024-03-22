using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviour{
    public Image healthBar;
    public static float maxEnemyHP;
    public static float currentEnemyHP;
    private float lerpSpeed; 
    public TextMeshProUGUI healthText;


    void Start(){
        currentEnemyHP = 15f;
        maxEnemyHP = 15f;
        lerpSpeed = 3f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update(){
        if(currentEnemyHP < 0) {
            currentEnemyHP = 0;
        }
        enemyHealthFiller();
    }

      public void enemyHealthFiller() {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentEnemyHP/ maxEnemyHP, lerpSpeed);
        healthText.text = currentEnemyHP + "HP";
    }
}
