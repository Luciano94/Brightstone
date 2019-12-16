﻿using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour{
    
    private float currentLife = 100;
    [SerializeField] float life = 100.0f;
    [SerializeField] float atkMult = 1.0f;
    [SerializeField] int lostExpPercent = 40;
    private float atkDmg = 0.0f;
    private float experience = 0;
    private float experienceToAdd = 0;
    public float experienceToNextLevel;
    private int playerLevel = 0;
    private bool isLowHealth = false;
    private bool godMode = false;
    public bool IsLowHealth { get { return isLowHealth; } private set { isLowHealth = value; } }

    [HideInInspector][SerializeField] UnityEvent onHit;

    private void Start(){
        experienceToNextLevel = 100;
        Invoke("PlayerRespawn", 1.0f);
    }

    private void Update(){
        if (experienceToAdd > 0 && !GameManager.Instance.pause){
            Experience = 2;
            experienceToAdd -= 2;
        }
    }

    private void PlayerRespawn(){
        SoundManager.Instance.PlayerRespawn();
    }

    public int Defense{
        get{return 20;}
    }

    public float Life{
        get{return currentLife;}
        set{
            if(currentLife > 0 && !godMode){
                currentLife -= value;

                RunSaver.currentRun.data.damageRecieved += (uint)value;

                CheckLowHealth();
                
                if(currentLife <= 0){
                    GetComponent<PlayerCombat>().Death();
                    GameManager.Instance.PlayerDeath();
                }
                    
                else
                    OnHit.Invoke();
            }
        }
    }

    public int PlayerLevel{
        set{playerLevel = value;}
        get{return playerLevel;}
    }

    public float LifeStat{
        get{return currentLife;}
        set{
            if(value > 0){
                currentLife += value;
                life += value;
                CheckLowHealth();
            }
        }
    }

    public float setLifeStat{
        get{return life;}
        set{
            if(value > 0){
                currentLife = value;
                life = value;
                playerLevel--;
                CheckLowHealth();
                UIManager.Instance.ExpUpdate();
                UIManager.Instance.UpdateFillBar(life);
            }
        }
    }

    private void CheckLowHealth(){
        SoundManager.Instance.PlayerHP(currentLife / life);
        if (currentLife / life < 0.3f)
            isLowHealth = true;
        else
            isLowHealth = false;            
    }

    private int LifeNormalized(){
        int value = (int)(currentLife * 100); 
        return (int)(value / life); 
    }
    
    public float MaxLife() { return life; }

    public float AtkDmg{
        get{return atkDmg;}
        set{
            if(value > 0)
                atkDmg = value;
        }
    }

    public float AtkMult{
        get{return atkMult;}
        set{
            if(value > 0)
                atkMult += value;
                
        }
    }

    public float setAtkMult{
        get{return atkMult;}
        set{
            if(value > 0)
                atkMult = value;
                playerLevel--;
                UIManager.Instance.ExpUpdate();
        }

    }

    public void AddExperience(float experienceToAdd){
        this.experienceToAdd += experienceToAdd;
    }

    public float Experience{
        get{return experience;}
        set{
            if(value > 0){
                experience += value;
                if(experience >= experienceToNextLevel){
                    playerLevel++;
                    experience -= experienceToNextLevel;
                    experienceToNextLevel *= 2;
                    SoundManager.Instance.PlayerLvlUp();
                    UIManager.Instance.EnterMarket();
                }
                RunSaver.currentRun.data.expObtained += (uint)value;
                UIManager.Instance.ExpUpdate();
            }
            else 
                if(experience + value < 0)
                    experience = 0;
                else
                    experience += value;
        }
    }

    public float SetExperience{
        set{
            experience = value;
            RunSaver.currentRun.data.expObtained = (uint)value;
        }
    }

    public float Death(){
        float newExp = ((experience * 4.0f)/10.0f);
        if((experience - newExp) > 1.0f){
            experience -= newExp;
        }else{
            experience = 0.0f; 
        }
        return experience;
    }

    public void ChangeGodModeState(){
        godMode = !godMode;
    }

    public UnityEvent OnHit
    {
        get { return onHit; }
    }
}
