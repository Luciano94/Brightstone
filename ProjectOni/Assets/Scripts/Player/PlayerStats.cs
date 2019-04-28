using UnityEngine;

public class PlayerStats : MonoBehaviour{
    
    private float currentLife = 100;
    private float life = 100;
    private float atkDmg = 1;

    private float experience = 0;

    public float Life{
        get{return currentLife;}
        set{
            /*if(value < 0)
                if(currentLife + value <= 0){
                    //te moriste wey
                }else currentLife += value;
            else
                if(currentLife + value > life)
                    currentLife = life;
                else currentLife += value;*/
            if(life > 0)
                life -= value;
        }
    }

    public float LifeStat{
        get{return life;}
        set{
            if(value > 0)
                life += value;
        }
    }

    public float AtkDmg{
        get{return atkDmg;}
        set{
            if(value > 0)
                atkDmg += value;
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
}
