using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    public void Confirm(){
        MarketCanvasManager.Instance.Confirm();
    }

    public void PlusAttack(){
        MarketCanvasManager.Instance.AttackLevel(true);
    }

    public void MinusAttack(){
        MarketCanvasManager.Instance.AttackLevel(false);
    }
    
    public void PlusLife(){
        MarketCanvasManager.Instance.LifeLevel(true);
    }

    public void MinusLife(){
        MarketCanvasManager.Instance.LifeLevel(false);
    }
    
    public void PlusDefense(){
        MarketCanvasManager.Instance.DefenseLevel(true);
    }

    public void MinusDefense(){
        MarketCanvasManager.Instance.DefenseLevel(false);
    }
}
