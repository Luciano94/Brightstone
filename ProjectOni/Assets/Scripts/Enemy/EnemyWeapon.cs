﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    PlayerStats playerStats;
    EnemyStats enemyStats;

    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
        enemyStats = GameManager.Instance.enemySts;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        playerStats.Life = enemyStats.AtkDmg;
        UIManager.Instance.lifeUpdate();
    }
}