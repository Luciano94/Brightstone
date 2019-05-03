using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
    
    private float currentLife = 100;
    [SerializeField] float life = 100.0f;
    [SerializeField] float atkDmg = 25.0f;

    private float experience = 0;

    public float Life{
        get{return currentLife;}
        set{
            if(life > 0){
                life -= value;
                if(life <= 0){
                    SceneManager.LoadScene(0);
                }
            }
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
