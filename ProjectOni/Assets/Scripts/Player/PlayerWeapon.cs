using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    PlayerStats playerStats;
    EnemyStats enemyStats;

    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
        enemyStats = GameManager.Instance.enemySts;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(playerStats.AtkDmg + "ontrigger");
        enemyStats.Life = playerStats.AtkDmg;
    }
}