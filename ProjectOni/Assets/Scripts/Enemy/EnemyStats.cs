﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    //private float currentLife = 100;
    [SerializeField] float life = 50;
    [SerializeField] float atkDmg = 10.0f;

    private float experience = 0;

    public float Life {
        get { return life; }
        set {
            if (life > 0)
                life -= value;
            else
                gameObject.SetActive(false);
        }
    }

    public float AtkDmg {
        get { return atkDmg; }
        set {
            if (value > 0)
                atkDmg += value;
        }
    }
}