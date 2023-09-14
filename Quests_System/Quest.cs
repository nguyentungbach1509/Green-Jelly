using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {
    public string qst_title;
    public string qst_description;
    public int qst_gold;
    public float qst_exp;
    public QuestGoal qst_goal;

}