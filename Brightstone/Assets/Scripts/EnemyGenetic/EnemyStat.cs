using UnityEngine;

public class EnemyStat : MonoBehaviour{

    public EnemyType enemyType;
    int level= 1;
    int life = 50;
    int movSpeed = 50;
    int atkDmg = 20;
    MonoBehaviour[] movSet; 

    public int AtkDmg{
        get{return atkDmg;}
    }

    public void Hit(){
        EnemyManager.Instance.PlusPercent(enemyType);
    }
}
