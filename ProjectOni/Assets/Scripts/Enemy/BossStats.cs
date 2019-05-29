using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossStats : MonoBehaviour {
    [SerializeField] float life = 500;
    [SerializeField] float atkDmg = 20.0f;
    
    private float currentLife;
    private float experience = 100;
    GameObject myRoom;
    public EnemyType enemyType;
    MonoBehaviour[] movSet; 

    [HideInInspector][SerializeField] UnityEvent onHit;
    [HideInInspector][SerializeField] UnityEvent onParried;

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
                DamagePopup.Create(transform.position, (int)value);
            }
            else {
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath();
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                GameManager.Instance.PLayerWin();
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

    public void Parried(){
        onParried.Invoke();
    }

    public UnityEvent OnHit
    {
        get { return onHit; }
    }

    public UnityEvent OnParried
    {
        get { return onParried; }
    }
}