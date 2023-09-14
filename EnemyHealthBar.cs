using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
Handle health bar UI of enemies
*/
public class EnemyHealthBar : MonoBehaviour
{
   public Slider slider;
   public Text textName;
   public Text textLevel;

   public void setMaxHealth(int health) {
       slider.maxValue = health;
       slider.value = health;
   }

   public void setHealth(int health) {
       slider.value = health;
   }


    public void hideHealthBar() {
        DestroyImmediate(gameObject, true);
    }

    public void setText(string name, int level) {
        textName.text = name;
        textLevel.text = level + "";
    }
}
