using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum QuestType {
    Kill,
    Collect
}

[System.Serializable]
public class QuestGoal {
    public QuestType type;
    public int currentQuantity;
    public int requiredQuantity;

    public bool QuestPass() {
        return (currentQuantity >= requiredQuantity);
    }

    public void KilledEnemies() {
        if(type == QuestType.Kill) {
            currentQuantity++;
        }
    }

}