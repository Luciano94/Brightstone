using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField]private int lvl = 1;
    [SerializeField]private float currentExp = 0;
    [SerializeField]private float requiredExp = 100;

    public int Level{
        get{return lvl;}
    }

    public float Experience{
        get{return currentExp;}
        
        set{
            if(currentExp + value >= requiredExp){
                lvl++;
                currentExp = 0;
                requiredExp *= lvl; 
            }else{
                currentExp += value;
            }
        }
    }

    public float RequiredExp{
        get{return requiredExp;}
    }

    public void GetTocken(){
        if(lvl-1 > 0)
            lvl--;
    }
}
