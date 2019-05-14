using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossStats : MonoBehaviour {
    [SerializeField] float life = 300;
    [SerializeField] float atkDmg = 20.0f;
    
    private float currentLife;
    private float experience = 100;
    GameObject myRoom;
    public EnemyType enemyType;
    MonoBehaviour[] movSet; 

    [HideInInspector][SerializeField] UnityEvent onHit;

    private void Awake() {
        currentLife = life;
    }

    public GameObject MyRoom {
        get { return myRoom; }
        set { myRoom = value; }
    }

    public float Life {
        get { return currentLife; }
        set {
            currentLife -= value;
            if (currentLife >= 0) {
                OnHit.Invoke();
                UIManager.Instance.BossDamaged();
            }
            else {
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath();
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                Destroy(gameObject);
            }
        }
    }
    
    public float MaxLife() { return life; }

    public float AtkDmg {
        get { return atkDmg; }
        set {
            if (value > 0)
                atkDmg += value;
        }
    }

    public void Hit(){
        EnemyManager.Instance.PlusPercent(enemyType);
    }

    public UnityEvent OnHit
    {
        get { return onHit; }
    }
}