using UnityEngine;

public class PlayerStats : MonoBehaviour{
    
    private float currentLife = 100;
    [SerializeField] float life = 100.0f;
    [SerializeField] float atkMult = 1.0f;
    [SerializeField] int lostExpPercent = 40;
    float atkDmg = 0.0f;


    private float experience = 0;

    public float Life{
        get{return currentLife;}
        set{
            if(currentLife > 0){
                currentLife -= value;
                if(currentLife <= 0){
                    GameManager.Instance.PlayerDeath();
                }
            }
        }
    }

    public float LifeStat{
        get{return currentLife;}
        set{
            if(value > 0)
            {
                currentLife += value;
                life += value;
            }
        }
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

    public float Experience{
        get{return experience;}
        set{
            if(value > 0)
                experience += value;
            else 
                if(experience + value < 0)
                    experience = 0;
                else
                    experience += value;
        }
    }

    public float SetExperience{
        set{experience = value;}
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
}
