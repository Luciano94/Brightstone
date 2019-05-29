﻿using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour {
    [SerializeField] float life = 50;
    [SerializeField] float atkDmg = 10.0f;
    
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
                DamagePopup.Create(transform.position, (int)value);
            }
            else {
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath();
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                Destroy(gameObject);
            }
        }
    }

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