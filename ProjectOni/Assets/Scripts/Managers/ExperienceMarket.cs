using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceMarket : MonoBehaviour
{
    private static ExperienceMarket instance;

    public static ExperienceMarket Instance {
        get {
            instance = FindObjectOfType<ExperienceMarket>();
            if(instance == null) {
                GameObject go = new GameObject("ExperienceMarket");
                instance = go.AddComponent<ExperienceMarket>();
            }
            return instance;
        }
    }

    private int level;
    private int requiredExp;

    private PlayerStats playerStats;

    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
        level = 1;
        requiredExp = 100;
        Debug.Log(level + " / " + requiredExp);
    }

    public void LifeUp(){
        if(playerStats.Experience >= requiredExp){
            playerStats.Experience = -requiredExp;
            level ++;
            requiredExp += requiredExp * level;
            playerStats.LifeStat = 50;
            UIManager.Instance.lifeUpdate();
            Debug.Log(level + " / " + requiredExp);
        }
    }

    public void AtkUp(){
        if(playerStats.Experience >= requiredExp){
            playerStats.Experience = -requiredExp;
            level ++;
            requiredExp += requiredExp * level;
            playerStats.AtkDmg = 1;
            UIManager.Instance.atkUpdate();
            Debug.Log(level + " / " + requiredExp);
        }
    }
}
