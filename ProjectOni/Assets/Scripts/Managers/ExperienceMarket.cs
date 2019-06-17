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

    public string Required{
        get{return ("Level: " + level + " \nExp Required: " + requiredExp);}
    }

    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
        level = 1;
        requiredExp = 100;
    }

    public void LifeUp(){
        if(playerStats.Experience >= requiredExp){
            playerStats.Experience = -requiredExp;
            level ++;
            requiredExp = requiredExp * level;
            playerStats.LifeStat = 50;
            UIManager.Instance.lifeUpdate();
            UIManager.Instance.MarketUpdate();
        }
    }

    public void AtkUp(){
        if(playerStats.Experience >= requiredExp){
            playerStats.Experience = -requiredExp;
            level ++;
            requiredExp += requiredExp * level;
            playerStats.AtkMult = 0.25f;
            UIManager.Instance.atkUpdate();
            UIManager.Instance.MarketUpdate();
        }
    }
}
